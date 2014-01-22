$RTBPref::Loadout::Primary[0] = "";
$RTBPref::Loadout::Primary[1] = "";
$RTBPref::Loadout::Primary[2] = "";
$RTBPref::Loadout::Primary[3] = "";
$RTBPref::Loadout::Primary[4] = "";
$RTBPref::Loadout::Primary[5] = "";
$RTBPref::Loadout::Primary[6] = "";
$RTBPref::Loadout::Primary[7] = "";
$RTBPref::Loadout::Primary[8] = "";
$RTBPref::Loadout::Primary[9] = "";
$RTBPref::Loadout::Primary[10] = "";
$RTBPref::Loadout::Primary[11] = "";
$RTBPref::Loadout::Primary[12] = "";
$RTBPref::Loadout::Primary[13] = "";

$RTBPref::Loadout::Secondary[0] = "";
$RTBPref::Loadout::Secondary[1] = "";
$RTBPref::Loadout::Secondary[2] = "";
$RTBPref::Loadout::Secondary[3] = "";
$RTBPref::Loadout::Secondary[4] = "";
$RTBPref::Loadout::Secondary[5] = "";
$RTBPref::Loadout::Secondary[6] = "";
$RTBPref::Loadout::Secondary[7] = "";
$RTBPref::Loadout::Secondary[8] = "";
$RTBPref::Loadout::Secondary[9] = "";
$RTBPref::Loadout::Secondary[10] = "";
$RTBPref::Loadout::Secondary[11] = "";
$RTBPref::Loadout::Secondary[12] = "";
$RTBPref::Loadout::Secondary[13] = "";

$RTBPref::Loadout::Tertiary[0] = "";
$RTBPref::Loadout::Tertiary[1] = "";
$RTBPref::Loadout::Tertiary[2] = "";
$RTBPref::Loadout::Tertiary[3] = "";
$RTBPref::Loadout::Tertiary[4] = "";
$RTBPref::Loadout::Tertiary[5] = "";
$RTBPref::Loadout::Tertiary[6] = "";
$RTBPref::Loadout::Tertiary[7] = "";
$RTBPref::Loadout::Tertiary[8] = "";
$RTBPref::Loadout::Tertiary[9] = "";
$RTBPref::Loadout::Tertiary[10] = "";
$RTBPref::Loadout::Tertiary[11] = "";
$RTBPref::Loadout::Tertiary[12] = "";
$RTBPref::Loadout::Tertiary[13] = "";

$RTBPref::Loadout::PrimaryMaxNumber = 0;
$RTBPref::Loadout::SecondaryMaxNumber = 0;
$RTBPref::Loadout::TertiaryMaxNumber = 0;

for(%i = 0; %i <= 13; %i++)
{
	if(!$RTBPref::Loadout::Primary[%i] == "")
	{
		$RTBPref::Loadout::PrimaryMaxNumber++;
	}
	
	if(!$RTBPref::Loadout::Secondary[%i] == "")
	{
		#RTBPref::Loadout::SecondaryMaxNumber++;
	}
	
	if(!$RTBPref::Loadout::Tertiary[%i] == "")
	{
		#RTBPref::Loadout::TertiaryMaxNumber++;
	}
}

function servercmdListPrimary(%client)
{
	if ($RTBPref::Loadout::PrimaryMaxNumber == 0)
	{
		messageclient(%client, "","\c6There are no Primary Items available at the time.");
		return;
	}

	for(%i = 0; %i < $RTBPref::Loadout::PrimaryMaxNumber; %i++) 
		messageclient(%client, "","(" @ %i+1 @ ")\c7" SPC $RTBPref::Loadout::Primary[%i]);
}


// +-----------------------------------+
// |  LIST THE SECONDARY ITEM CHOICES  |
// +-----------------------------------+
function servercmdListSecondary(%client)
{
	if ($SecondaryItemNum == 0)
	{
		messageclient(%client, "","Sorry, there are no secondary weapons to choose from!");
		return;
	}

	for(%i = 0; %i < $SecondaryItemNum; %i++) 
		messageclient(%client, "","(" @ %i+1 @ ")\c7" SPC $SecondaryItem[%i]);
}

// +----------------------------------+
// |  LIST THE TERTIARY ITEM CHOICES  |
// +----------------------------------+
function servercmdListTertiary(%client)
{
	if ($TertiaryItemNum == 0)
	{
		messageclient(%client, "","Sorry, there are no tertiary weapons to choose from!");
		return;
	}
	
	for(%i = 0; %i < $TertiaryItemNum; %i++) 
		messageclient(%client, "","(" @ %i+1 @ ")\c7" SPC $TertiaryItem[%i]);
}

// +-------------------------+
// |  CHOOSE A PRIMARY ITEM  |
// +-------------------------+
function servercmdChoosePrimary(%client,%choice)
{
	if (%choice >= 1 && %choice <= $PrimaryItemNum)
	{
		%client.defaultPrimary = $PrimaryItem[%choice - 1];
		messageclient(%client, "","\c3Default Primary weapon has been set! Default Primary weapon will take effect by next spawn!");
	}
	else
		messageclient(%client, "","Sorry, that is an out-of-range choice!");
}

// +---------------------------+
// |  CHOOSE A SECONDARY ITEM  |
// +---------------------------+
function servercmdChooseSecondary(%client,%choice)
{
	if (%choice >= 1 && %choice <= $SecondaryItemNum)
	{
		%client.defaultSecondary = $SecondaryItem[%choice - 1];
		messageclient(%client, "","\c3Default Secondary weapon has been set! Default Secondary weapon will take effect by next spawn!");
	}
	else
		messageclient(%client, "","Sorry, that is an out-of-range choice!");
}

// +--------------------------+
// |  CHOOSE A TERTIARY ITEM  |
// +--------------------------+
function servercmdChooseTertiary(%client,%choice)
{
	if (%choice >= 1 && %choice <= $TertiaryItemNum)
	{
		%client.defaultTertiary = $TertiaryItem[%choice - 1];
		messageclient(%client, "","\c3Default Tertiary weapon has been set! Default Tertiary weapon will take effect by next spawn!");
	}
	else
		messageclient(%client, "","Sorry, that is an out-of-range choice!");
}

// +-------------------------+
// |  REMOVE DEFAULT LOADOUT |
// +-------------------------+
function servercmdCancelLoadout(%client)
{
	if (%client.defaultPrimary $= "" && %client.defaultSecondary $= "" && %client.defaultTertiary $= "")
		messageclient(%client, "","You do not have a default loadout to cancel!");
	else
	{
		%client.defaultPrimary = "";
		%client.defaultSecondary = "";
		%client.defaultTertiary = "";

		messageclient(%client, "","Your default loadout has been canceled! Changes will not take effect until next spawn.");
	}
}


package Default_Loadout
{

	// +----------------------------------------------------+
	// | This function is part of the Team Spawning system. |
	// +----------------------------------------------------+
	function GameConnection::SpawnPlayer(%client)
	{
		Parent::SpawnPlayer(%client);

		if (!(%client.defaultPrimary $= ""))
		{
			%client.player.tool[0] = %client.defaultPrimary.getID();
			messageClient(%client,'MsgItemPickup','',0,%client.defaultPrimary.getID());
		}

		if (!(%client.defaultSecondary $= ""))
		{
			%client.player.tool[1] = %client.defaultSecondary.getID();
			messageClient(%client,'MsgItemPickup','',1,%client.defaultSecondary.getID());
		}

		if (!(%client.defaultTertiary $= ""))
		{
			%client.player.tool[2] = %client.defaultTertiary.getID();
			messageClient(%client,'MsgItemPickup','',2,%client.defaultTertiary.getID());
		}
	}
	
};
activatepackage(Default_Loadout);
