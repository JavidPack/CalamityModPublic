using CalamityMod.Items.Accessories;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.Dyes.HairDye;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.Fountains;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalamityMod.NPCs
{
	public class CalamityGlobalTownNPC
	{
		#region Town NPC Patreon Name Sets
		private const int AnglerVanillaNames = 22;
		private static readonly string[] AnglerNames =
		{
			"Dazren",
		};
		private const int ArmsDealerVanillaNames = 24;
		private static readonly string[] ArmsDealerNames =
		{
			"Drifter",
			"Finchi",
		};
		private const int ClothierVanillaNames = 25;
		private static readonly string[] ClothierNames =
		{
			"Joeseph Jostar",
		};
		private const int CyborgVanillaNames = 22;
		private static readonly string[] CyborgNames = null;
		private const int DemolitionistVanillaNames = 22;
		private static readonly string[] DemolitionistNames = null;
		private const int DryadVanillaNames = 21;
		private static readonly string[] DryadNames =
		{
			"Rythmi",
			"Izuna",
			"Jasmine", // <@!430532867479699456> (phantasmagoria#6777)
		};
		private const int DyeTraderVanillaNames = 16;
		private static readonly string[] DyeTraderNames = null;
		private const int GoblinTinkererVanillaNames = 25;
		private static readonly string[] GoblinTinkererNames =
		{
			"Verth",
		};
		private const int GuideVanillaNames = 34;
		private static readonly string[] GuideNames =
		{
			"Lapp",
			"Ben Shapiro",
			"StreakistYT",
			"Necroplasmic",
			"Devin",
			"Woffle", // <@!185980979427540992> (Chipbeam#2268)
			"Cameron", // <@!340401981711712258> (CammyWammy#8634)
		};
		private const int MechanicVanillaNames = 24;
		private static readonly string[] MechanicNames =
		{
			"Lilly",
			"Daawn", // <@!206162323541458944> (Daawnily#3859)
			"Robin", // <@!654737510030639112> (Altzeus#8687)
		};
		private const int MerchantVanillaNames = 23;
		private static readonly string[] MerchantNames = null;
		private const int NurseVanillaNames = 24;
		private static readonly string[] NurseNames =
		{
			"Farsni",
		};
		private const int PainterVanillaNames = 18;
		private static readonly string[] PainterNames = null;
		private const int PartyGirlVanillaNames = 17;
		private static readonly string[] PartyGirlNames = null;
		private const int PirateVanillaNames = 11;
		private static readonly string[] PirateNames =
		{
			"Tyler Van Hook",
		};
		private const int SteampunkerVanillaNames = 20;
		private static readonly string[] SteampunkerNames =
		{
			"Vorbis",
			"Angel",
		};
		private const int StylistVanillaNames = 20;
		private static readonly string[] StylistNames =
		{
			"Amber", // <@!114677116473180169> (Mishiro Usui#1295)
		};
		private const int TavernkeepVanillaNames = 16;
		private static readonly string[] TavernkeepNames = null;
		private const int TaxCollectorVanillaNames = 19;
		private static readonly string[] TaxCollectorNames =
		{
			"Emmett",
		};
		private const int TruffleVanillaNames = 12;
		private static readonly string[] TruffleNames = null;
		private const int WitchDoctorVanillaNames = 10;
		private static readonly string[] WitchDoctorNames =
		{
			"Sok'ar",
			"Toxin", // <@!348174404984766465> (Toxin#9598)
		};
		private const int WizardVanillaNames = 22;
		private static readonly string[] WizardNames =
		{
			"Mage One-Trick",
			"Inorim, son of Ivukey",
			"Jensen",
			"Merasmus", // <@!288066987819663360> (Spider pee pee#3328)
		};
		#endregion


		#region Town NPC Names
		public static void ResetTownNPCNameBools(NPC npc, Mod mod)
		{
			void ResetName(int npcID, ref bool nameBool)
			{
				if (NPC.FindFirstNPC(npcID) == -1)
					nameBool = false;
			}

			ResetName(NPCID.Angler, ref CalamityWorld.anglerName);
			ResetName(NPCID.ArmsDealer, ref CalamityWorld.armsDealerName);
			ResetName(NPCID.Clothier, ref CalamityWorld.clothierName);
			ResetName(NPCID.Cyborg, ref CalamityWorld.cyborgName);
			ResetName(NPCID.Demolitionist, ref CalamityWorld.demolitionistName);
			ResetName(NPCID.Dryad, ref CalamityWorld.dryadName);
			ResetName(NPCID.DyeTrader, ref CalamityWorld.dyeTraderName);
			ResetName(NPCID.GoblinTinkerer, ref CalamityWorld.goblinTinkererName);
			ResetName(NPCID.Guide, ref CalamityWorld.guideName);
			ResetName(NPCID.Mechanic, ref CalamityWorld.mechanicName);
			ResetName(NPCID.Merchant, ref CalamityWorld.merchantName);
			ResetName(NPCID.Nurse, ref CalamityWorld.nurseName);
			ResetName(NPCID.Painter, ref CalamityWorld.painterName);
			ResetName(NPCID.PartyGirl, ref CalamityWorld.partyGirlName);
			ResetName(NPCID.Pirate, ref CalamityWorld.pirateName);
			ResetName(NPCID.Steampunker, ref CalamityWorld.steampunkerName);
			ResetName(NPCID.Stylist, ref CalamityWorld.stylistName);
			ResetName(NPCID.DD2Bartender, ref CalamityWorld.tavernkeepName);
			ResetName(NPCID.TaxCollector, ref CalamityWorld.taxCollectorName);
			ResetName(NPCID.Truffle, ref CalamityWorld.truffleName);
			ResetName(NPCID.WitchDoctor, ref CalamityWorld.witchDoctorName);
			ResetName(NPCID.Wizard, ref CalamityWorld.wizardName);
		}

		// Annoyingly, because npc.GivenName is a property, it can't be passed as a ref parameter.
		private static string ChooseName(ref bool alreadySet, string currentName, int numVanillaNames, string[] patreonNames)
		{
			if (alreadySet || patreonNames is null || patreonNames.Length == 0)
			{
				alreadySet = true;
				return currentName;
			} 

			alreadySet = true;
			int index = Main.rand.Next(numVanillaNames + patreonNames.Length);

			// If the roll isn't low enough, then a "vanilla name" was picked, meaning we change nothing.
			if (index >= patreonNames.Length)
				return currentName;

			// Change the name to be a randomly selected Patreon name if the roll is low enough.
			return patreonNames[index];
		}

		public static void SetPatreonTownNPCName(NPC npc, Mod mod)
		{
			if (npc.Calamity().setNewName)
			{
				npc.Calamity().setNewName = false;
				switch (npc.type)
				{
					case NPCID.Angler:
						npc.GivenName = ChooseName(ref CalamityWorld.anglerName, npc.GivenName, AnglerVanillaNames, AnglerNames);
						break;
					case NPCID.ArmsDealer:
						npc.GivenName = ChooseName(ref CalamityWorld.armsDealerName, npc.GivenName, ArmsDealerVanillaNames, ArmsDealerNames);
						break;
					case NPCID.Clothier:
						npc.GivenName = ChooseName(ref CalamityWorld.clothierName, npc.GivenName, ClothierVanillaNames, ClothierNames);
						break;
					case NPCID.Cyborg:
						npc.GivenName = ChooseName(ref CalamityWorld.cyborgName, npc.GivenName, CyborgVanillaNames, CyborgNames);
						break;
					case NPCID.Demolitionist:
						npc.GivenName = ChooseName(ref CalamityWorld.demolitionistName, npc.GivenName, DemolitionistVanillaNames, DemolitionistNames);
						break;
					case NPCID.Dryad:
						npc.GivenName = ChooseName(ref CalamityWorld.dryadName, npc.GivenName, DryadVanillaNames, DryadNames);
						break;
					case NPCID.DyeTrader:
						npc.GivenName = ChooseName(ref CalamityWorld.dyeTraderName, npc.GivenName, DyeTraderVanillaNames, DyeTraderNames);
						break;
					case NPCID.GoblinTinkerer:
						npc.GivenName = ChooseName(ref CalamityWorld.goblinTinkererName, npc.GivenName, GoblinTinkererVanillaNames, GoblinTinkererNames);
						break;
					case NPCID.Guide:
						npc.GivenName = ChooseName(ref CalamityWorld.guideName, npc.GivenName, GuideVanillaNames, GuideNames);
						break;
					case NPCID.Mechanic:
						npc.GivenName = ChooseName(ref CalamityWorld.mechanicName, npc.GivenName, MechanicVanillaNames, MechanicNames);
						break;
					case NPCID.Merchant:
						npc.GivenName = ChooseName(ref CalamityWorld.merchantName, npc.GivenName, MerchantVanillaNames, MerchantNames);
						break;
					case NPCID.Nurse:
						npc.GivenName = ChooseName(ref CalamityWorld.nurseName, npc.GivenName, NurseVanillaNames, NurseNames);
						break;
					case NPCID.Painter:
						npc.GivenName = ChooseName(ref CalamityWorld.painterName, npc.GivenName, PainterVanillaNames, PainterNames);
						break;
					case NPCID.PartyGirl:
						npc.GivenName = ChooseName(ref CalamityWorld.partyGirlName, npc.GivenName, PartyGirlVanillaNames, PartyGirlNames);
						break;
					case NPCID.Pirate:
						npc.GivenName = ChooseName(ref CalamityWorld.pirateName, npc.GivenName, PirateVanillaNames, PirateNames);
						break;
					case NPCID.Steampunker:
						npc.GivenName = ChooseName(ref CalamityWorld.steampunkerName, npc.GivenName, SteampunkerVanillaNames, SteampunkerNames);
						break;
					case NPCID.Stylist:
						npc.GivenName = ChooseName(ref CalamityWorld.stylistName, npc.GivenName, StylistVanillaNames, StylistNames);
						break;
					case NPCID.DD2Bartender: // Tavernkeep
						npc.GivenName = ChooseName(ref CalamityWorld.tavernkeepName, npc.GivenName, TavernkeepVanillaNames, TavernkeepNames);
						break;
					case NPCID.TaxCollector:
						npc.GivenName = ChooseName(ref CalamityWorld.taxCollectorName, npc.GivenName, TaxCollectorVanillaNames, TaxCollectorNames);
						break;
					case NPCID.Truffle:
						npc.GivenName = ChooseName(ref CalamityWorld.truffleName, npc.GivenName, TruffleVanillaNames, TruffleNames);
						break;
					case NPCID.WitchDoctor:
						npc.GivenName = ChooseName(ref CalamityWorld.witchDoctorName, npc.GivenName, WitchDoctorVanillaNames, WitchDoctorNames);
						break;
					case NPCID.Wizard:
						npc.GivenName = ChooseName(ref CalamityWorld.wizardName, npc.GivenName, WizardVanillaNames, WizardNames);
						break;
					default:
						break;
				}
			}
		}
		#endregion

		#region NPC New Shop Alert
		public static void TownNPCAlertSystem(NPC npc, Mod mod, SpriteBatch spriteBatch)
		{
			if (CalamityConfig.Instance.ShopNewAlert && npc.townNPC)
			{
				if (npc.type == NPCType<DILF>() && Main.LocalPlayer.Calamity().newPermafrostInventory)
				{
					DrawNewInventoryAlert(npc);
				}
				else if (npc.type == NPCType<FAP>() && Main.LocalPlayer.Calamity().newCirrusInventory)
				{
					DrawNewInventoryAlert(npc);
				}
				else if (npc.type == NPCType<SEAHOE>() && Main.LocalPlayer.Calamity().newAmidiasInventory)
				{
					DrawNewInventoryAlert(npc);
				}
				else if (npc.type == NPCType<THIEF>() && Main.LocalPlayer.Calamity().newBanditInventory)
				{
					DrawNewInventoryAlert(npc);
				}
				else
				{
					switch (npc.type)
					{
						case NPCID.Merchant:
							if (Main.LocalPlayer.Calamity().newMerchantInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Painter:
							if (Main.LocalPlayer.Calamity().newPainterInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.DyeTrader:
							if (Main.LocalPlayer.Calamity().newDyeTraderInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.PartyGirl:
							if (Main.LocalPlayer.Calamity().newPartyGirlInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Stylist:
							if (Main.LocalPlayer.Calamity().newStylistInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Demolitionist:
							if (Main.LocalPlayer.Calamity().newDemolitionistInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Dryad:
							if (Main.LocalPlayer.Calamity().newDryadInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.DD2Bartender:
							if (Main.LocalPlayer.Calamity().newTavernkeepInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.ArmsDealer:
							if (Main.LocalPlayer.Calamity().newArmsDealerInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.GoblinTinkerer:
							if (Main.LocalPlayer.Calamity().newGoblinTinkererInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.WitchDoctor:
							if (Main.LocalPlayer.Calamity().newWitchDoctorInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Clothier:
							if (Main.LocalPlayer.Calamity().newClothierInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Mechanic:
							if (Main.LocalPlayer.Calamity().newMechanicInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Pirate:
							if (Main.LocalPlayer.Calamity().newPirateInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Truffle:
							if (Main.LocalPlayer.Calamity().newTruffleInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Wizard:
							if (Main.LocalPlayer.Calamity().newWizardInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Steampunker:
							if (Main.LocalPlayer.Calamity().newSteampunkerInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.Cyborg:
							if (Main.LocalPlayer.Calamity().newCyborgInventory)
								DrawNewInventoryAlert(npc);
							break;
						case NPCID.SkeletonMerchant:
							if (Main.LocalPlayer.Calamity().newSkeletonMerchantInventory)
								DrawNewInventoryAlert(npc);
							break;
						default:
							break;
					}
				}

				void DrawNewInventoryAlert(NPC npc2)
				{
					// The position where the display is drawn
					Vector2 drawPos = npc2.Center - Main.screenPosition;

					// The height of a single frame of the npc
					float npcHeight = (float)(Main.npcTexture[npc2.type].Height / Main.npcFrameCount[npc2.type] / 2) * npc2.scale;

					// Offset the debuff display based on the npc's graphical offset, and 16 units, to create some space between the sprite and the display
					float drawPosY = npcHeight + npc.gfxOffY + 36f;

					// Texture animation variables
					Texture2D texture = GetTexture("CalamityMod/ExtraTextures/UI/NPCAlertDisplay");
					npc.Calamity().shopAlertAnimTimer++;
					if (npc.Calamity().shopAlertAnimTimer >= 6)
					{
						npc.Calamity().shopAlertAnimTimer = 0;

						npc.Calamity().shopAlertAnimFrame++;
						if (npc.Calamity().shopAlertAnimFrame > 4)
							npc.Calamity().shopAlertAnimFrame = 0;
					}
					int frameHeight = texture.Height / 5;
					Rectangle animRect = new Rectangle(0, frameHeight * npc.Calamity().shopAlertAnimFrame, texture.Width, frameHeight);

					spriteBatch.Draw(texture, drawPos - new Vector2(5f, drawPosY), animRect, Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
				}
			}
		}

		public static void DisableAlert(NPC npc, Mod mod)
		{
			if (npc.townNPC)
			{
				switch (npc.type)
				{
					case NPCID.Merchant:
						Main.LocalPlayer.Calamity().newMerchantInventory = false;
						break;
					case NPCID.Painter:
						Main.LocalPlayer.Calamity().newPainterInventory = false;
						break;
					case NPCID.DyeTrader:
						Main.LocalPlayer.Calamity().newDyeTraderInventory = false;
						break;
					case NPCID.PartyGirl:
						Main.LocalPlayer.Calamity().newPartyGirlInventory = false;
						break;
					case NPCID.Stylist:
						Main.LocalPlayer.Calamity().newStylistInventory = false;
						break;
					case NPCID.Demolitionist:
						Main.LocalPlayer.Calamity().newDemolitionistInventory = false;
						break;
					case NPCID.Dryad:
						Main.LocalPlayer.Calamity().newDryadInventory = false;
						break;
					case NPCID.DD2Bartender:
						Main.LocalPlayer.Calamity().newTavernkeepInventory = false;
						break;
					case NPCID.ArmsDealer:
						Main.LocalPlayer.Calamity().newArmsDealerInventory = false;
						break;
					case NPCID.GoblinTinkerer:
						Main.LocalPlayer.Calamity().newGoblinTinkererInventory = false;
						break;
					case NPCID.WitchDoctor:
						Main.LocalPlayer.Calamity().newWitchDoctorInventory = false;
						break;
					case NPCID.Clothier:
						Main.LocalPlayer.Calamity().newClothierInventory = false;
						break;
					case NPCID.Mechanic:
						Main.LocalPlayer.Calamity().newMechanicInventory = false;
						break;
					case NPCID.Pirate:
						Main.LocalPlayer.Calamity().newPirateInventory = false;
						break;
					case NPCID.Truffle:
						Main.LocalPlayer.Calamity().newTruffleInventory = false;
						break;
					case NPCID.Wizard:
						Main.LocalPlayer.Calamity().newWizardInventory = false;
						break;
					case NPCID.Steampunker:
						Main.LocalPlayer.Calamity().newSteampunkerInventory = false;
						break;
					case NPCID.Cyborg:
						Main.LocalPlayer.Calamity().newCyborgInventory = false;
						break;
					case NPCID.SkeletonMerchant:
						Main.LocalPlayer.Calamity().newSkeletonMerchantInventory = false;
						break;
					default:
						break;
				}
			}
		}

		public static void SetNewShopVariable(int[] types, bool alreadySet)
		{
			if (!alreadySet)
			{
				for (int i = 0; i < types.Length; i++)
				{
					if (types[i] == NPCType<DILF>())
					{
						Main.LocalPlayer.Calamity().newPermafrostInventory = true;
					}
					else if (types[i] == NPCType<FAP>())
					{
						Main.LocalPlayer.Calamity().newCirrusInventory = true;
					}
					else if (types[i] == NPCType<SEAHOE>())
					{
						Main.LocalPlayer.Calamity().newAmidiasInventory = true;
					}
					else if (types[i] == NPCType<THIEF>())
					{
						Main.LocalPlayer.Calamity().newBanditInventory = true;
					}
					else
					{
						switch (types[i])
						{
							case NPCID.Merchant:
								Main.LocalPlayer.Calamity().newMerchantInventory = true;
								break;
							case NPCID.Painter:
								Main.LocalPlayer.Calamity().newPainterInventory = true;
								break;
							case NPCID.DyeTrader:
								Main.LocalPlayer.Calamity().newDyeTraderInventory = true;
								break;
							case NPCID.PartyGirl:
								Main.LocalPlayer.Calamity().newPartyGirlInventory = true;
								break;
							case NPCID.Stylist:
								Main.LocalPlayer.Calamity().newStylistInventory = true;
								break;
							case NPCID.Demolitionist:
								Main.LocalPlayer.Calamity().newDemolitionistInventory = true;
								break;
							case NPCID.Dryad:
								Main.LocalPlayer.Calamity().newDryadInventory = true;
								break;
							case NPCID.DD2Bartender:
								Main.LocalPlayer.Calamity().newTavernkeepInventory = true;
								break;
							case NPCID.ArmsDealer:
								Main.LocalPlayer.Calamity().newArmsDealerInventory = true;
								break;
							case NPCID.GoblinTinkerer:
								Main.LocalPlayer.Calamity().newGoblinTinkererInventory = true;
								break;
							case NPCID.WitchDoctor:
								Main.LocalPlayer.Calamity().newWitchDoctorInventory = true;
								break;
							case NPCID.Clothier:
								Main.LocalPlayer.Calamity().newClothierInventory = true;
								break;
							case NPCID.Mechanic:
								Main.LocalPlayer.Calamity().newMechanicInventory = true;
								break;
							case NPCID.Pirate:
								Main.LocalPlayer.Calamity().newPirateInventory = true;
								break;
							case NPCID.Truffle:
								Main.LocalPlayer.Calamity().newTruffleInventory = true;
								break;
							case NPCID.Wizard:
								Main.LocalPlayer.Calamity().newWizardInventory = true;
								break;
							case NPCID.Steampunker:
								Main.LocalPlayer.Calamity().newSteampunkerInventory = true;
								break;
							case NPCID.Cyborg:
								Main.LocalPlayer.Calamity().newCyborgInventory = true;
								break;
							case NPCID.SkeletonMerchant:
								Main.LocalPlayer.Calamity().newSkeletonMerchantInventory = true;
								break;
							default:
								break;
						}
					}
				}
			}
		}
		#endregion

		#region NPC Chat
		public static void NewNPCQuotes(NPC npc, Mod mod, ref string chat)
		{
			int fapsol = NPC.FindFirstNPC(NPCType<FAP>());
			int permadong = NPC.FindFirstNPC(NPCType<DILF>());
			int seahorse = NPC.FindFirstNPC(NPCType<SEAHOE>());
			int thief = NPC.FindFirstNPC(NPCType<THIEF>());
			int angelstatue = NPC.FindFirstNPC(NPCID.Merchant);

			switch (npc.type)
			{
				case NPCID.Guide:
					if (Main.hardMode)
					{
						if (Main.rand.NextBool(20))
						{
							chat = "Could you be so kind as to, ah...check hell for me...? I left someone I kind of care about down there.";
						}

						if (Main.rand.NextBool(20))
						{
							chat = "I have this sudden shiver up my spine, like a meteor just fell and thousands of innocent creatures turned into monsters from the stars.";
						}
					}

					if (Main.rand.NextBool(20) && NPC.downedMoonlord)
					{
						chat = "The dungeon seems even more restless than usual, watch out for the powerful abominations stirring up in there.";
					}

					if (CalamityWorld.downedProvidence)
					{
						if (Main.rand.NextBool(20))
						{
							chat = "Seems like extinguishing that butterfly caused its life to seep into the hallowed areas, try taking a peek there and see what you can find!";
						}

						if (Main.rand.NextBool(20))
						{
							chat = "I've heard there is a portal of antimatter absorbing everything it can see in the dungeon, try using the Rune of Kos there!";
						}
					}

					break;
				case NPCID.Truffle:
					if (Main.rand.NextBool(8))
					{
						chat = "I don't feel very safe; I think there's pigs following me around and it frightens me.";
					}

					if (NPC.AnyNPCs(NPCType<FAP>()))
					{
						chat = "Sometimes, " + Main.npc[fapsol].GivenName + " just looks at me funny and I'm not sure how I feel about that.";
					}

					break;

				case NPCID.Angler:
					if (Main.rand.NextBool(5) && NPC.AnyNPCs(NPCType<SEAHOE>()))
					{
						chat = "Someone tell " + Main.npc[seahorse].GivenName + " to quit trying to throw me out of town, it's not going to work.";
					}

					break;

				case NPCID.TravellingMerchant:
					if (Main.rand.NextBool(5) && NPC.AnyNPCs(NPCType<FAP>()) && NPC.AnyNPCs(NPCID.Merchant))
					{
						chat = "Tell " + Main.npc[fapsol].GivenName + " I'll take up her offer and meet with her at the back of " + Main.npc[angelstatue].GivenName + "'s house.";
					}

					break;

				case NPCID.SkeletonMerchant:
					if (Main.rand.NextBool(5))
					{
						chat = "What'dya buyin'?";
					}

					break;

				case NPCID.WitchDoctor:
					if (Main.rand.NextBool(8) && Main.LocalPlayer.ZoneJungle)
					{
						chat = "My home here has an extensive history and a mysterious past. You'll find out quickly just how extensive it is...";
					}

					if (Main.rand.NextBool(8) && Main.LocalPlayer.ZoneJungle &&
						Main.hardMode && !NPC.downedPlantBoss)
					{
						chat = "I have unique items if you show me that you have bested the guardian of this jungle.";
					}

					if (Main.rand.NextBool(8) && Main.bloodMoon)
					{
						chat = "This is as good a time as any to pick up the best ingredients for potions.";
					}

					break;

				case NPCID.PartyGirl:
					if (Main.rand.NextBool(10) && fapsol != -1)
					{
						chat = "I have a feeling we're going to have absolutely fantastic parties with " + Main.npc[fapsol].GivenName + " around!";
					}

					if (Main.eclipse)
					{
						if (Main.rand.NextBool(5))
						{
							chat = "I think my light display is turning into an accidental bug zapper. At least the monsters are enjoying it.";
						}

						if (Main.rand.NextBool(5))
						{
							chat = "Ooh! I love parties where everyone wears a scary costume!";
						}
					}

					break;

				case NPCID.Painter:
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneCorrupt)
					{
						chat = "A little sickness isn't going to stop me from doing my work as an artist!";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneCrimson)
					{
						chat = "There's a surprising art to this area. A sort of horrifying, eldritch feeling. It inspires me!";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneSnow)
					{
						if (NPC.AnyNPCs(NPCType<DILF>()) && Main.rand.NextBool(2))
						{
							chat = "Think " + Main.npc[permadong].GivenName + " would let me paint him like one of his French girls?!";
						}
						else
						{
							chat = "I'm not exactly suited for this cold weather. Still looks pretty, though.";
						}
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneDesert)
					{
						chat = "I hate sand. It's coarse, and rough and gets in my paint.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneHoly)
					{
						chat = "Do you think unicorn blood could be used as a good pigment or resin? No I'm not going to find out myself.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneSkyHeight)
					{
						//chat = "I can't breathe."
						chat = "I can't work in this environment. All of my paint just floats off.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneJungle)
					{
						chat = "Painting the tortoises in a still life isn't going so well.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneAstral)
					{
						chat = "I can't paint a still life if the fruit grows legs and walks away.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneUnderworldHeight)
					{
						chat = "On the canvas, things get heated around here all the time. Like the environment!";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneUnderworldHeight)
					{
						chat = "Sorry, I'm all out of watercolors. They keep evaporating.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneCalamity)
					{
						chat = "Roses, really? That's such an overrated thing to paint.";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneSulphur)
					{
						chat = "Fun fact! Sulphur was used as pigment once upon a time! Or was it Cinnabar?";
					}
					if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneAbyss)
					{
						chat = "Easiest landscape I've ever painted in my life.";
					}
					break;

				case NPCID.Wizard:
					if (Main.rand.NextBool(10) && permadong != -1)
					{
						chat = "I'd let " + Main.npc[permadong].GivenName + " coldheart MY icicle.";
					}

					if (Main.rand.NextBool(10) && Main.hardMode)
					{
						chat = "Space just got way too close for comfort.";
					}

					break;

				case NPCID.Dryad:
					if (Main.rand.NextBool(5) && CalamityWorld.buffedEclipse && Main.eclipse)
					{
						chat = "There's a dark solar energy emanating from the moths that appear during this time. Ah, the moths as you progress further get more powerful...hmm...what power was Yharon holding back?";
					}

					if (Main.rand.NextBool(5) && Main.hardMode)
					{
						chat = "That starborne illness sits upon this land like a blister. Do even more vile forces of corruption exist in worlds beyond?";
					}

					if (Main.rand.NextBool(5) && NPC.AnyNPCs(NPCType<FAP>()) && Main.LocalPlayer.ZoneGlowshroom)
					{
						chat = Main.npc[fapsol].GivenName + " put me up to this.";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneSulphur)
					{
						chat = "My ancestor was lost here long ago. I must pay my respects to her.";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.ZoneGlowshroom)
					{
						//high iq drugs iirc
						chat = "I'm not here for any reason! Just picking up mushrooms for uh, later use.";
					}

					break;

				case NPCID.Stylist:
					string worldEvil = WorldGen.crimson ? "Crimson" : "Corruption";
					if (Main.rand.NextBool(15) && Main.hardMode)
					{
						chat = "Please don't catch space lice. Or " + worldEvil + " lice. Or just lice in general.";
					}

					if (fapsol != -1)
					{
						if (Main.rand.NextBool(15))
						{
							chat = "Sometimes I catch " + Main.npc[fapsol].GivenName + " sneaking up from behind me.";
						}

						if (Main.rand.NextBool(15))
						{
							chat = Main.npc[fapsol].GivenName + " is always trying to brighten my mood...even if, deep down, I know she's sad.";
						}
					}
					if ((npc.GivenName == "Amber" ? Main.rand.NextBool(10) : Main.rand.NextBool(15)) && Main.LocalPlayer.Calamity().pArtifact)
					{
						if (Main.LocalPlayer.Calamity().profanedCrystalBuffs)
						{
							chat = Main.rand.NextBool(2) ? "They look so cute and yet, I can feel their immense power just by being near them. What are you?" : "I hate to break it to you, but you don't have hair to cut or style, hun.";
						}
						else if (Main.LocalPlayer.Calamity().gDefense && Main.LocalPlayer.Calamity().gOffense)
						{
							chat = "Aww, they're so cute, do they have names?"; 
						}
					}
					break;

				case NPCID.GoblinTinkerer:
					if (Main.rand.NextBool(3) && thief != -1 && CalamityWorld.Reforges >= 1)
					{
						chat = $"Hey, is it just me or have my pockets gotten lighter ever since " + Main.npc[thief].GivenName + " arrived?";
					}
					if (Main.rand.NextBool(10) && NPC.downedMoonlord)
					{
						chat = "You know...we haven't had an invasion in a while...";
					}

					break;

				case NPCID.ArmsDealer:
					if (Main.rand.NextBool(5) && Main.eclipse)
					{
						chat = "That's the biggest moth I've ever seen for sure. You'd need one big gun to take one of those down.";
					}

					if (Main.rand.NextBool(10) && CalamityWorld.downedDoG)
					{
						chat = "Is it me or are your weapons getting bigger and bigger?";
					}

					break;

				case NPCID.Merchant:
					if (Main.rand.NextBool(5) && NPC.downedMoonlord)
					{
						chat = "Each night seems only more foreboding than the last. I feel unthinkable terrors are watching your every move.";
					}

					if (Main.rand.NextBool(5) && Main.eclipse)
					{
						chat = "Are you daft?! Turn off those lamps!";
					}

					if (Main.rand.NextBool(5) && Main.raining && Main.LocalPlayer.Calamity().ZoneSulphur)
					{
						chat = "If this acid rain keeps up, there'll be a shortage of Dirt Blocks soon enough!";
					}

					if (Main.rand.NextBool(7) && thief != -1)
					{
						chat = "I happen to have several Angel Statues at the moment, a truely rare commodity. Want one?";
					}

					if (Main.rand.NextBool(7) && CalamityWorld.death)
					{
						chat = "The caverns have become increasingly dark as of late, so I stocked up on some special torches if you have the funds.";
					}

					break;

				case NPCID.Mechanic:
					if (Main.rand.NextBool(5) && NPC.downedMoonlord)
					{
						chat = "What do you mean your traps aren't making the cut? Don't look at me!";
					}

					if (Main.rand.NextBool(5) && Main.eclipse)
					{
						chat = "Um...should my nightlight be on?";
					}

					if (Main.rand.NextBool(5) && fapsol != -1)
					{
						chat = "Well, I like " + Main.npc[fapsol].GivenName + ", but I, ah...I have my eyes on someone else.";
					}

					if (Main.rand.NextBool(5) && CalamityWorld.rainingAcid)
					{
						chat = "Maybe I should've waterproofed my gadgets... They're starting to corrode.";
					}

					break;

				case NPCID.DD2Bartender:
					if (Main.rand.NextBool(5) && !Main.dayTime && Main.moonPhase == 0)
					{
						chat = "Care for a little Moonshine?";
					}

					if (Main.rand.NextBool(10) && fapsol != -1)
					{
						chat = "Sheesh, " + Main.npc[fapsol].GivenName + " is a little cruel, isn't she? I never claimed to be an expert on anything but ale!";
					}

					break;

				case NPCID.Pirate:
					if (Main.rand.NextBool(5) && !CalamityWorld.downedLeviathan)
					{
						chat = "Aye, I've heard of a mythical creature in the oceans, singing with an alluring voice. Careful when yer fishin out there.";
					}

					if (Main.rand.NextBool(5) && CalamityWorld.downedAquaticScourge)
					{
						chat = "I have to thank ye again for takin' care of that sea serpent. Or was that another one...";
					}

					if (Main.rand.NextBool(5) && NPC.AnyNPCs(NPCType<SEAHOE>()))
					{
						chat = "I remember legends about that " + Main.npc[seahorse].GivenName + ". He ain't quite how the stories make him out to be though.";
					}

					if (Main.rand.NextBool(5) && NPC.AnyNPCs(NPCType<FAP>()))
					{
						chat = "Twenty-nine bottles of beer on the wall...";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.Center.ToTileCoordinates().X < 380 && !Main.LocalPlayer.Calamity().ZoneSulphur)
					{
						chat = "Now this is a scene that I can admire any time! I feel like something is watching me though.";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneSulphur)
					{
						chat = "It ain't much of a sight, but there's still life living in these waters.";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneSulphur)
					{
						chat = "Me ship might just sink from the acid alone.";
					}
					break;

				case NPCID.Cyborg:
					if (Main.rand.NextBool(10) && Main.raining)
					{
						chat = "All these moments will be lost in time. Like tears...in the rain.";
					}

					if (NPC.downedMoonlord)
					{
						if (Main.rand.NextBool(10))
						{
							chat = "Always shoot for the moon! It has clearly worked before.";
						}

						if (Main.rand.NextBool(10))
						{
							chat = "Draedon? He's...a little 'high octane' if you know what I mean.";
						}
					}

					if (Main.rand.NextBool(10) && !CalamityWorld.downedPlaguebringer && NPC.downedGolemBoss)
					{
						chat = "Those oversized bugs terrorizing the jungle... Surely you of all people could shut them down!";
					}

					break;

				case NPCID.Clothier:
					if (NPC.downedMoonlord)
					{
						if (Main.rand.NextBool(10))
						{
							chat = "Who you gonna call?";
						}

						if (Main.rand.NextBool(10))
						{
							chat = "Those screams...I'm not sure why, but I feel like a nameless fear has awoken in my heart.";
						}

						if (Main.rand.NextBool(10))
						{
							chat = "I can faintly hear ghostly shrieks from the dungeon...and not ones I'm familiar with at all. Just what is going on in there?";
						}
					}

					if (Main.rand.NextBool(10) && CalamityWorld.downedPolterghast)
					{
						chat = "Whatever that thing was, I'm glad it's gone now.";
					}

					if (Main.rand.NextBool(5) && NPC.AnyNPCs(NPCID.MoonLordCore))
					{
						chat = "Houston, we've had a problem.";
					}

					break;

				case NPCID.Steampunker:
					bool hasPortalGun = false;
					for (int k = 0; k < Main.maxPlayers; k++)
					{
						Player player = Main.player[k];
						if (player.active && player.HasItem(ItemID.PortalGun))
						{
							hasPortalGun = true;
						}
					}

					if (Main.rand.NextBool(5) && hasPortalGun)
					{
						chat = "Just what is that contraption? It makes my Teleporters look like child's play!";
					}

					if (Main.rand.NextBool(5) && NPC.downedMoonlord)
					{
						chat = "Yep! I'm also considering being a space pirate now.";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneAstral)
					{
						chat = "Some of my machines are starting to go haywire thanks to this Astral Infection. I probably shouldn't have built them here";
					}

					if (Main.rand.NextBool(5) && Main.LocalPlayer.ZoneHoly)
					{
						chat = "I'm sorry I really don't have any Unicorn proof tech here, you're on your own.";
					}

					break;

				case NPCID.DyeTrader:
					if (Main.rand.NextBool(5))
					{
						chat = "Have you seen those gemstone creatures in the caverns? Their colors are simply breathtaking!";
					}

					if (Main.rand.NextBool(5) && permadong != -1)
					{
						chat = "Do you think " + Main.npc[permadong].GivenName + " knows how to 'let it go?'";
					}

					break;

				case NPCID.TaxCollector:
					int platinumCoins = 0;
					for (int k = 0; k < Main.maxPlayers; k++)
					{
						Player player = Main.player[k];
						if (player.active)
						{
							for (int j = 0; j < player.inventory.Length; j++)
							{
								if (player.inventory[j].type == ItemID.PlatinumCoin)
								{
									platinumCoins += player.inventory[j].stack;
								}
							}
						}
					}

					if (Main.rand.NextBool(5) && platinumCoins >= 100)
					{
						chat = "BAH! Doesn't seem like I'll ever be able to quarrel with the debts of the town again!";
					}

					if (Main.rand.NextBool(5) && platinumCoins >= 500)
					{
						chat = "Where and how are you getting all of this money?";
					}

					if (Main.rand.NextBool(5) && !CalamityWorld.downedBrimstoneElemental)
					{
						chat = "Perhaps with all that time you've got you could check those old ruins? Certainly something of value in it for you!";
					}

					if (Main.rand.NextBool(10) && CalamityWorld.downedDoG)
					{
						chat = "Devourer of what, you said? Devourer of Funds, if its payroll is anything to go by!";
					}

					if (Main.rand.NextBool(10) && CalamityUtils.InventoryHas(Main.LocalPlayer, ItemType<SlickCane>()))
					{
						chat = "Goodness! That cane has swagger!";
					}

					break;

				case NPCID.Demolitionist:
					if (Main.rand.NextBool(5) && CalamityWorld.downedDoG)
					{
						chat = "God Slayer Dynamite? Boy do I like the sound of that!";
					}

					break;

				default:
					break;
			}
		}
		#endregion

		#region NPC Stat Changes
		public static void MakeTownNPCsTakeMoreDamage(NPC npc, Projectile projectile, Mod mod, ref int damage)
		{
			if (npc.townNPC && projectile.hostile)
				damage *= 2;
		}

		public static void NPCStatBuffs(Mod mod, ref float damageMult, ref int defense)
		{
			if (NPC.downedMoonlord)
			{
				damageMult += 0.6f;
				defense += 20;
			}
			if (CalamityWorld.downedProvidence)
			{
				damageMult += 0.2f;
				defense += 12;
			}
			if (CalamityWorld.downedPolterghast)
			{
				damageMult += 0.2f;
				defense += 12;
			}
			if (CalamityWorld.downedDoG)
			{
				damageMult += 0.2f;
				defense += 12;
			}
			if (CalamityWorld.downedYharon)
			{
				damageMult += 0.2f;
				defense += 12;
			}
			if (CalamityWorld.downedSCal)
			{
				damageMult += 0.6f;
				defense += 20;
			}
		}
		#endregion

		#region Shop Stuff
		public static void ShopSetup(int type, Mod mod, ref Chest shop, ref int nextSlot)
		{
			if (type == NPCID.Merchant)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.Flare, (Main.LocalPlayer.HasItem(ItemType<FirestormCannon>()) || Main.LocalPlayer.HasItem(ItemType<SpectralstormCannon>())) && !Main.LocalPlayer.HasItem(ItemID.FlareGun));
				SetShopItem(ref shop, ref nextSlot, ItemID.BlueFlare, (Main.LocalPlayer.HasItem(ItemType<FirestormCannon>()) || Main.LocalPlayer.HasItem(ItemType<SpectralstormCannon>())) && !Main.LocalPlayer.HasItem(ItemID.FlareGun));
				SetShopItem(ref shop, ref nextSlot, ItemID.ApprenticeBait, NPC.downedBoss1);
				SetShopItem(ref shop, ref nextSlot, ItemID.JourneymanBait, NPC.downedBoss3);
				SetShopItem(ref shop, ref nextSlot, WorldGen.crimson ? ItemID.Vilethorn : ItemID.CrimsonRod, WorldGen.shadowOrbSmashed || NPC.downedBoss2);
				SetShopItem(ref shop, ref nextSlot, WorldGen.crimson ? ItemID.BallOHurt : ItemID.TheRottedFork, WorldGen.shadowOrbSmashed || NPC.downedBoss2);
				SetShopItem(ref shop, ref nextSlot, ItemID.MasterBait, NPC.downedPlantBoss);
				SetShopItem(ref shop, ref nextSlot, ItemID.AngelStatue, NPC.FindFirstNPC(NPCType<THIEF>()) != -1, Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemID.UltrabrightTorch, CalamityWorld.death);
			}

			// Because of the defiled condition, the dye trader does not receive an alert icon when hardmode starts.
			if (type == NPCID.DyeTrader)
			{
				SetShopItem(ref shop, ref nextSlot, ItemType<DefiledFlameDye>(), Main.hardMode && CalamityWorld.defiled, Item.buyPrice(0, 10));
			}

			if (type == NPCID.ArmsDealer)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.Stake, Main.LocalPlayer.HasItem(ItemType<Impaler>()));
				SetShopItem(ref shop, ref nextSlot, WorldGen.crimson ? ItemID.Musket : ItemID.TheUndertaker, WorldGen.shadowOrbSmashed || NPC.downedBoss2);
				SetShopItem(ref shop, ref nextSlot, ItemID.Boomstick, NPC.downedQueenBee, price: Item.buyPrice(0, 20, 0, 0));
				SetShopItem(ref shop, ref nextSlot, ItemID.TacticalShotgun, NPC.downedGolemBoss, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.SniperRifle, NPC.downedGolemBoss, Item.buyPrice(0, 25));
			}

			if (type == NPCID.Stylist)
			{
				SetShopItem(ref shop, ref nextSlot, ItemType<StealthHairDye>(), Main.LocalPlayer.Calamity().rogueStealthMax > 0f && Main.LocalPlayer.Calamity().wearingRogueArmor);
				SetShopItem(ref shop, ref nextSlot, ItemType<WingTimeHairDye>(), Main.LocalPlayer.wingTimeMax > 0);
				SetShopItem(ref shop, ref nextSlot, ItemType<AdrenalineHairDye>(), CalamityWorld.revenge && CalamityConfig.Instance.Rippers);
				SetShopItem(ref shop, ref nextSlot, ItemType<RageHairDye>(), CalamityWorld.revenge && CalamityConfig.Instance.Rippers);
			}

			if (type == NPCID.Cyborg)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.RocketLauncher, NPC.downedGolemBoss, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemType<LionHeart>(), CalamityWorld.downedPolterghast);
			}

			if (type == NPCID.Pirate)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.PirateMap, price: Item.buyPrice(gold: 5));
			}

			if (type == NPCID.Dryad)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.JungleRose, price: Item.buyPrice(0, 2));
				SetShopItem(ref shop, ref nextSlot, ItemID.NaturesGift, price: Item.buyPrice(0, 10));
				SetShopItem(ref shop, ref nextSlot, ItemID.SlimeCrown, NPC.downedSlimeKing && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 2));
				SetShopItem(ref shop, ref nextSlot, ItemID.SuspiciousLookingEye, NPC.downedBoss1 && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 3));
				SetShopItem(ref shop, ref nextSlot, ItemType<DecapoditaSprout>(), CalamityWorld.downedCrabulon, Item.buyPrice(0, 4));
				SetShopItem(ref shop, ref nextSlot, ItemID.BloodySpine, NPC.downedBoss2 && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 6));
				SetShopItem(ref shop, ref nextSlot, ItemID.WormFood, NPC.downedBoss2 && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 6));
				SetShopItem(ref shop, ref nextSlot, WorldGen.crimson ? ItemID.BandofStarpower : ItemID.PanicNecklace, WorldGen.shadowOrbSmashed || NPC.downedBoss2);
				SetShopItem(ref shop, ref nextSlot, WorldGen.crimson ? ItemID.WormScarf : ItemID.BrainOfConfusion, Main.expertMode && NPC.downedBoss2);
				SetShopItem(ref shop, ref nextSlot, ItemType<BloodyWormFood>(), CalamityWorld.downedPerforator, Item.buyPrice(0, 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<RottenBrain>(), CalamityWorld.downedPerforator && Main.expertMode);
				SetShopItem(ref shop, ref nextSlot, ItemType<Teratoma>(), CalamityWorld.downedHiveMind, Item.buyPrice(0, 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<BloodyWormTooth>(), CalamityWorld.downedHiveMind && Main.expertMode);
				SetShopItem(ref shop, ref nextSlot, ItemType<OverloadedSludge>(), CalamityWorld.downedSlimeGod, Item.buyPrice(0, 15));
				SetShopItem(ref shop, ref nextSlot, ItemType<RomajedaOrchid>());
			}

			if (type == NPCID.GoblinTinkerer)
			{
				SetShopItem(ref shop, ref nextSlot, ItemType<MeleeLevelMeter>(), price: Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemType<RangedLevelMeter>(), price: Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemType<MagicLevelMeter>(), price: Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemType<SummonLevelMeter>(), price: Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemType<RogueLevelMeter>(), price: Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemType<StatMeter>(), price: Item.buyPrice(1));
				SetShopItem(ref shop, ref nextSlot, ItemID.GoblinBattleStandard, price: Item.buyPrice(0, 1));
			}

			if (type == NPCID.Clothier)
			{
				SetShopItem(ref shop, ref nextSlot, ItemType<BlueBrickWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<BlueSlabWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<BlueTiledWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<GreenBrickWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<GreenSlabWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<GreenTiledWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<PinkBrickWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<PinkSlabWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<PinkTiledWallUnsafe>(), price: Item.buyPrice(copper: 10));
				SetShopItem(ref shop, ref nextSlot, ItemID.GoldenKey, Main.hardMode, Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemID.PumpkinMoonMedallion, NPC.downedHalloweenKing, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.NaughtyPresent, NPC.downedChristmasIceQueen, Item.buyPrice(0, 25));
			}

			if (type == NPCID.Painter)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.PainterPaintballGun, price: Item.buyPrice(0, 15));
			}

			if (type == NPCID.Steampunker)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.MechanicalWorm, NPC.downedMechBoss1 && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 20));
				SetShopItem(ref shop, ref nextSlot, ItemID.MechanicalEye, NPC.downedMechBoss2 && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 20));
				SetShopItem(ref shop, ref nextSlot, ItemID.MechanicalSkull, NPC.downedMechBoss3 && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 20));
				SetShopItem(ref shop, ref nextSlot, ItemType<AstralSolution>(), price: Item.buyPrice(0, 0, 5));
			}

			if (type == NPCID.Wizard)
			{
				SetShopItem(ref shop, ref nextSlot, ItemType<HowlsHeart>());
				SetShopItem(ref shop, ref nextSlot, ItemType<CharredIdol>(), CalamityWorld.downedBrimstoneElemental, Item.buyPrice(0, 20));
				SetShopItem(ref shop, ref nextSlot, ItemType<AstralChunk>(), CalamityWorld.downedAstrageldon, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.MagicMissile, price: Item.buyPrice(0, 5));
				SetShopItem(ref shop, ref nextSlot, ItemID.SpectreStaff, NPC.downedGolemBoss, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.InfernoFork, NPC.downedGolemBoss, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.ShadowbeamStaff, NPC.downedGolemBoss, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.CelestialSigil, NPC.downedMoonlord && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(3));
				SetShopItem(ref shop, ref nextSlot, ItemType<ProfanedShard>(), CalamityWorld.downedGuardians, Item.buyPrice(5));
			}

			if (type == NPCID.WitchDoctor)
			{
				SetShopItem(ref shop, ref nextSlot, ItemType<SunkenSeaFountain>());
				SetShopItem(ref shop, ref nextSlot, ItemType<SulphurousFountainItem>());
				SetShopItem(ref shop, ref nextSlot, ItemType<AbyssFountainItem>(), Main.hardMode);
				SetShopItem(ref shop, ref nextSlot, ItemType<AstralFountainItem>(), Main.hardMode);
				SetShopItem(ref shop, ref nextSlot, ItemID.Abeemination, CalamityConfig.Instance.SellVanillaSummons, price: Item.buyPrice(0, 8));
				SetShopItem(ref shop, ref nextSlot, ItemType<BulbofDoom>(), NPC.downedPlantBoss && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 20));
				SetShopItem(ref shop, ref nextSlot, ItemID.SolarTablet, NPC.downedGolemBoss, Item.buyPrice(0, 25));
				SetShopItem(ref shop, ref nextSlot, ItemID.LihzahrdPowerCell, NPC.downedGolemBoss && CalamityConfig.Instance.SellVanillaSummons, Item.buyPrice(0, 30));
				SetShopItem(ref shop, ref nextSlot, ItemType<GypsyPowder>(), NPC.downedGolemBoss, Item.buyPrice(0, 10));
				SetShopItem(ref shop, ref nextSlot, ItemType<AncientMedallion>(), CalamityWorld.downedScavenger, Item.buyPrice(0, 50));
				SetShopItem(ref shop, ref nextSlot, ItemType<Abomination>(), CalamityWorld.downedPlaguebringer, Item.buyPrice(0, 50));
				SetShopItem(ref shop, ref nextSlot, ItemType<BirbPheromones>(), CalamityWorld.downedBumble, Item.buyPrice(5));
			}

			if (type == NPCID.SkeletonMerchant)
			{
				SetShopItem(ref shop, ref nextSlot, ItemID.Marrow, Main.hardMode, Item.buyPrice(0, 36));
			}
		}

		public static void TravelingMerchantShop(Mod mod, ref int[] shop, ref int nextSlot)
		{
			if (Main.moonPhase == 0)
			{
				shop[nextSlot] = ItemType<FrostBarrier>();
				nextSlot++;
			}
		}

		public static void SetShopItem(ref Chest shop, ref int nextSlot, int itemID, bool condition = true, int? price = null)
		{
			if (condition)
			{
				shop.item[nextSlot].SetDefaults(itemID);
				if (price != null)
				{
					shop.item[nextSlot].shopCustomPrice = price;
				}

				nextSlot++;
			}
		}
		#endregion
	}
}