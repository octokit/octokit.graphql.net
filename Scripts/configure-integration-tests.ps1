
function SetVariable([string]$key, [string]$value)
{
    [environment]::SetEnvironmentVariable($key, $value, "User")
    [environment]::SetEnvironmentVariable($key, $value)
}

function AskYesNoQuestion([string]$question, [string]$key)
{
	$answer = Read-Host -Prompt ($question + " Press Y to set this, otherwise we'll skip it")
	if ($answer -eq "Y")
	{
		SetVariable $key "YES"
	}
	else
	{
		SetVariable $key $null
	}
	
	Write-Host
	
	return ($answer -eq "Y")
}

function VerifyEnvironmentVariable([string]$friendlyName, [string]$key, [bool]$optional = $false)
{
	if ($optional -eq $true)
	{
		$label = "(optional)"
	}
	else
	{
		$label = "(required)"
	}

	$existing_value = [environment]::GetEnvironmentVariable($key,"User")
	if ($existing_value -eq $null)
	{
		$value = Read-Host -Prompt "Set the $friendlyName to use for the integration tests $label"
		SetVariable $key $value
	}
	else
	{
		Write-Host "$existing_value found as the configured $friendlyName"
		if ($optional -eq $true)
		{
			$clear = Read-Host -Prompt 'Want to remove or change this optional value, press Y'
			if ($clear -eq "Y")
			{
				$reset = Read-Host -Prompt "Press R to remove and Press C to change the value, otherwise we'll move on"
				if ($reset -eq "C")
				{
					$value = Read-Host -Prompt "Change the $friendlyName to use for the integration tests"
       				SetVariable $key $value
				}
				elseif ($reset -eq "R")
				{
					SetVariable $key $null
				}
			}
		}
		else
		{
			$reset = Read-Host -Prompt "Press Y to change this value, otherwise we'll move on "
			if ($reset -eq "Y")
			{
				$value = Read-Host -Prompt "Change the $friendlyName to use for the integration tests"
       			SetVariable $key $value
			}
		}
	}

	Write-Host
}

Write-Host
Write-Host "BIG FREAKING WARNING!!!!!" 
Write-Host "You should use a test account when running the Octokit integration tests!"
Write-Host
Write-Host

VerifyEnvironmentVariable "account name" "OCTOKIT_GQL_GITHUBUSERNAME"
VerifyEnvironmentVariable "account password" "OCTOKIT_GQL_GITHUBPASSWORD" $true
VerifyEnvironmentVariable "OAuth token" "OCTOKIT_GQL_OAUTHTOKEN"