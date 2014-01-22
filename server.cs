// +------------------------+
// |  CREATE THE DATABASES  |
// +------------------------+
function loadDatabases()
{

	// +------------------------------+
	// |  LOAD PRIMARY ITEM DATABASE  |
	// +------------------------------+
	$PrimaryItemNum = 0;
	if(isFile("Add-Ons/Weapon_GCatsFirearms/server.cs"))
	{
		$PrimaryItem[$PrimaryItemNum] = "gc_famasItem";
		$PrimaryItemNum++;
			
		$PrimaryItem[$PrimaryItemNum] = "gc_fnscarItem";
		$PrimaryItemNum++;
	
		$PrimaryItem[$PrimaryItemNum] = "gc_frf2Item";
		$PrimaryItemNum++;
	
		$PrimaryItem[$PrimaryItemNum] = "gc_as50Item";
		$PrimaryItemNum++;
	
	}
	


	// +--------------------------------+
	// |  LOAD SECONDARY ITEM DATABASE  |
	// +--------------------------------+
	$SecondaryItemNum = 0;
	if(isFile("Add-Ons/Weapon_GCatsFirearms/server.cs"))
	{
		$SecondaryItem[$SecondaryItemNum] = "gc_browningItem";
		$SecondaryItemNum++;
	
		$SecondaryItem[$SecondaryItemNum] = "gc_browning_suppressedItem";
		$SecondaryItemNum++;
		
		$SecondaryItem[$SecondaryItemNum] = "gc_mp5kItem";
		$SecondaryItemNum++;
	
		$SecondaryItem[$SecondaryItemNum] = "gc_spas12Item";
		$SecondaryItemNum++;
	
	}
	


	// +-------------------------------+
	// |  LOAD TERTIARY ITEM DATABASE  |
	// +-------------------------------+
	$TertiaryItemNum = 0;
	if(isFile("Add-Ons/Weapon_GCatsFirearms/server.cs"))
	{
		$TertiaryItem[$TertiaryItemNum] = "gc_grapefruitItem";
		$TertiaryItemNum++;

		$TertiaryItem[$TertiaryItemNum] = "gc_rpg7Item";
		$TertiaryItemNum++;

		$TertiaryItem[$TertiaryItemNum] = "gc_axeItem";
		$TertiaryItemNum++;
		
	}

}

// +----------------------+
// |  LOAD THE DATABASES  |
// +----------------------+
loadDatabases();

// +---------------------------------+
// |  LIST THE PRIMARY ITEM CHOICES  |
// +---------------------------------+
function servercmdListPrimary(%client)
{
	if ($PrimaryItemNum == 0)
	{
		messageclient(%client, "","Sorry, there are no primary weapons to choose from!");
		return;
	}

	for(%i = 0; %i < $PrimaryItemNum; %i++) 
		messageclient(%client, "","(" @ %i+1 @ ")\c7" SPC $PrimaryItem[%i]);
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
