using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TankBattle;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TankBattleTestSuite
{

    class RequirementException : Exception
    {
        public RequirementException()
        {
        }

        public RequirementException(string message) : base(message)
        {
        }

        public RequirementException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    

    class Test
    {


        #region Testing Code

        private delegate bool TestCase();

        private static string ErrorDescription = null;

        private static void SetErrorDescription(string desc)
        {
            ErrorDescription = desc;
        }

        private static bool FloatEquals(float a, float b)
        {
            if (Math.Abs(a - b) < 0.01) return true;
            return false;
        }

        private static Dictionary<string, string> unitTestResults = new Dictionary<string, string>();

        private static void Passed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[passed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                throw new Exception("ErrorDescription found for passing test case");
            }
            Console.WriteLine();
        }
        private static void Failed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[failed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                Console.Write("\n{0}", ErrorDescription);
                ErrorDescription = null;
            }
            Console.WriteLine();
        }
        private static void FailedToMeetRequirement(string name, string comment)
        {
            Console.Write("[      ] ");
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("{0}", comment);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void DoTest(TestCase test)
        {
            // Have we already completed this test?
            if (unitTestResults.ContainsKey(test.Method.ToString()))
            {
                return;
            }

            bool passed = false;
            bool metRequirement = true;
            string exception = "";
            try
            {
                passed = test();
            }
            catch (RequirementException e)
            {
                metRequirement = false;
                exception = e.Message;
            }
            catch (Exception e)
            {
                exception = e.GetType().ToString();
            }

            string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
            string fnName = test.Method.ToString().Split('0')[1];

            if (metRequirement)
            {
                if (passed)
                {
                    unitTestResults[test.Method.ToString()] = "Passed";
                    Passed(string.Format("{0}.{1}", className, fnName), exception);
                }
                else
                {
                    unitTestResults[test.Method.ToString()] = "Failed";
                    Failed(string.Format("{0}.{1}", className, fnName), exception);
                }
            }
            else
            {
                unitTestResults[test.Method.ToString()] = "Failed";
                FailedToMeetRequirement(string.Format("{0}.{1}", className, fnName), exception);
            }
            Cleanup();
        }

        private static Stack<string> errorDescriptionStack = new Stack<string>();


        private static void Requires(TestCase test)
        {
            string result;
            bool wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

            if (!wasTested)
            {
                // Push the error description onto the stack (only thing that can change, not that it should)
                errorDescriptionStack.Push(ErrorDescription);

                // Do the test
                DoTest(test);

                // Pop the description off
                ErrorDescription = errorDescriptionStack.Pop();

                // Get the proper result for out
                wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

                if (!wasTested)
                {
                    throw new Exception("This should never happen");
                }
            }

            if (result == "Failed")
            {
                string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
                string fnName = test.Method.ToString().Split('0')[1];

                throw new RequirementException(string.Format("-> {0}.{1}", className, fnName));
            }
            else if (result == "Passed")
            {
                return;
            }
            else
            {
                throw new Exception("This should never happen");
            }

        }

        #endregion

        #region Test Cases
        private static Battle InitialiseGame()
        {
            Requires(TestBattle0Battle);
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);
            Requires(TestBattle0RegisterPlayer);

            Battle game = new Battle(2, 1);
            Chassis tank = Chassis.GetTank(1);
            GenericPlayer player1 = new PlayerController("player1", tank, Color.Orange);
            GenericPlayer player2 = new PlayerController("player2", tank, Color.Purple);
            game.RegisterPlayer(1, player1);
            game.RegisterPlayer(2, player2);
            return game;
        }
        private static void Cleanup()
        {
            while (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Dispose();
            }
        }
        private static bool TestBattle0Battle()
        {
            Battle game = new Battle(2, 1);
            return true;
        }
        private static bool TestBattle0NumPlayers()
        {
            Requires(TestBattle0Battle);

            Battle game = new Battle(2, 1);
            return game.NumPlayers() == 2;
        }
        private static bool TestBattle0GetRounds()
        {
            Requires(TestBattle0Battle);

            Battle game = new Battle(3, 5);
            return game.GetRounds() == 5;
        }
        private static bool TestBattle0RegisterPlayer()
        {
            Requires(TestBattle0Battle);
            Requires(TestChassis0GetTank);

            Battle game = new Battle(2, 1);
            Chassis tank = Chassis.GetTank(1);
            GenericPlayer player = new PlayerController("playerName", tank, Color.Orange);
            game.RegisterPlayer(1, player);
            return true;
        }
        private static bool TestBattle0GetPlayerNumber()
        {
            Requires(TestBattle0Battle);
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            Battle game = new Battle(2, 1);
            Chassis tank = Chassis.GetTank(1);
            GenericPlayer player = new PlayerController("playerName", tank, Color.Orange);
            game.RegisterPlayer(1, player);
            return game.GetPlayerNumber(1) == player;
        }
        private static bool TestBattle0PlayerColour()
        {
            Color[] arrayOfColours = new Color[8];
            for (int i = 0; i < 8; i++)
            {
                arrayOfColours[i] = Battle.PlayerColour(i + 1);
                for (int j = 0; j < i; j++)
                {
                    if (arrayOfColours[j] == arrayOfColours[i]) return false;
                }
            }
            return true;
        }
        private static bool TestBattle0CalculatePlayerPositions()
        {
            int[] positions = Battle.CalculatePlayerPositions(8);
            for (int i = 0; i < 8; i++)
            {
                if (positions[i] < 0) return false;
                if (positions[i] > 160) return false;
                for (int j = 0; j < i; j++)
                {
                    if (positions[j] == positions[i]) return false;
                }
            }
            return true;
        }
        private static bool TestBattle0Rearrange()
        {
            int[] ar = new int[100];
            for (int i = 0; i < 100; i++)
            {
                ar[i] = i;
            }
            Battle.Rearrange(ar);
            for (int i = 0; i < 100; i++)
            {
                if (ar[i] != i)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestBattle0NewGame()
        {
            Battle game = InitialiseGame();
            game.NewGame();

            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestBattle0GetMap()
        {
            Requires(TestBattlefield0Battlefield);
            Battle game = InitialiseGame();
            game.NewGame();
            Battlefield battlefield = game.GetMap();
            if (battlefield != null) return true;

            return false;
        }
        private static bool TestBattle0GetCurrentGameplayTank()
        {
            Requires(TestBattle0Battle);
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);
            Requires(TestBattle0RegisterPlayer);
            Requires(TestGameplayTank0GetPlayerNumber);

            Battle game = new Battle(2, 1);
            Chassis tank = Chassis.GetTank(1);
            GenericPlayer player1 = new PlayerController("player1", tank, Color.Orange);
            GenericPlayer player2 = new PlayerController("player2", tank, Color.Purple);
            game.RegisterPlayer(1, player1);
            game.RegisterPlayer(2, player2);

            game.NewGame();
            GameplayTank ptank = game.GetCurrentGameplayTank();
            if (ptank.GetPlayerNumber() != player1 && ptank.GetPlayerNumber() != player2)
            {
                return false;
            }
            if (ptank.GetTank() != tank)
            {
                return false;
            }

            return true;
        }

        private static bool TestChassis0GetTank()
        {
            Chassis tank = Chassis.GetTank(1);
            if (tank != null) return true;
            else return false;
        }
        private static bool TestChassis0DisplayTank()
        {
            Requires(TestChassis0GetTank);
            Chassis tank = Chassis.GetTank(1);

            int[,] tankGraphic = tank.DisplayTank(45);
            if (tankGraphic.GetLength(0) != 12) return false;
            if (tankGraphic.GetLength(1) != 16) return false;
            // We don't really care what the tank looks like, but the 45 degree tank
            // should at least look different to the -45 degree tank
            int[,] tankGraphic2 = tank.DisplayTank(-45);
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (tankGraphic2[y, x] != tankGraphic[y, x])
                    {
                        return true;
                    }
                }
            }

            SetErrorDescription("Tank with turret at -45 degrees looks the same as tank with turret at 45 degrees");

            return false;
        }
        private static void DisplayLine(int[,] array)
        {
            string report = "";
            report += "A line drawn from 3,0 to 0,3 on a 4x4 array should look like this:\n";
            report += "0001\n";
            report += "0010\n";
            report += "0100\n";
            report += "1000\n";
            report += "The one produced by Chassis.SetLine() looks like this:\n";
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    report += array[y, x] == 1 ? "1" : "0";
                }
                report += "\n";
            }
            SetErrorDescription(report);
        }
        private static bool TestChassis0SetLine()
        {
            int[,] ar = new int[,] { { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 } };
            Chassis.SetLine(ar, 3, 0, 0, 3);

            // Ideally, the line we want to see here is:
            // 0001
            // 0010
            // 0100
            // 1000

            // However, as we aren't that picky, as long as they have a 1 in every row and column
            // and nothing in the top-left and bottom-right corners

            int[] rows = new int[4];
            int[] cols = new int[4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (ar[y, x] == 1)
                    {
                        rows[y] = 1;
                        cols[x] = 1;
                    }
                    else if (ar[y, x] > 1 || ar[y, x] < 0)
                    {
                        // Only values 0 and 1 are permitted
                        SetErrorDescription(string.Format("Somehow the number {0} got into the array.", ar[y, x]));
                        return false;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (rows[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
                if (cols[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
            }
            if (ar[0, 0] == 1)
            {
                DisplayLine(ar);
                return false;
            }
            if (ar[3, 3] == 1)
            {
                DisplayLine(ar);
                return false;
            }

            return true;
        }
        private static bool TestChassis0GetTankArmour()
        {
            Requires(TestChassis0GetTank);
            // As long as it's > 0 we're happy
            Chassis tank = Chassis.GetTank(1);
            if (tank.GetTankArmour() > 0) return true;
            return false;
        }
        private static bool TestChassis0ListWeapons()
        {
            Requires(TestChassis0GetTank);
            // As long as there's at least one result and it's not null / a blank string, we're happy
            Chassis tank = Chassis.GetTank(1);
            if (tank.ListWeapons().Length == 0) return false;
            if (tank.ListWeapons()[0] == null) return false;
            if (tank.ListWeapons()[0] == "") return false;
            return true;
        }

        private static GenericPlayer CreateTestingPlayer()
        {
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            Chassis tank = Chassis.GetTank(1);
            GenericPlayer player = new PlayerController("player1", tank, Color.Aquamarine);
            return player;
        }

        private static bool TestGenericPlayer0PlayerController()
        {
            Requires(TestChassis0GetTank);

            Chassis tank = Chassis.GetTank(1);
            GenericPlayer player = new PlayerController("player1", tank, Color.Aquamarine);
            if (player != null) return true;
            return false;
        }
        private static bool TestGenericPlayer0GetTank()
        {
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            Chassis tank = Chassis.GetTank(1);
            GenericPlayer p = new PlayerController("player1", tank, Color.Aquamarine);
            if (p.GetTank() == tank) return true;
            return false;
        }
        private static bool TestGenericPlayer0PlayerName()
        {
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            const string PLAYER_NAME = "kfdsahskfdajh";
            Chassis tank = Chassis.GetTank(1);
            GenericPlayer p = new PlayerController(PLAYER_NAME, tank, Color.Aquamarine);
            if (p.PlayerName() == PLAYER_NAME) return true;
            return false;
        }
        private static bool TestGenericPlayer0GetColour()
        {
            Requires(TestChassis0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            Color playerColour = Color.Chartreuse;
            Chassis tank = Chassis.GetTank(1);
            GenericPlayer p = new PlayerController("player1", tank, playerColour);
            if (p.GetColour() == playerColour) return true;
            return false;
        }
        private static bool TestGenericPlayer0Winner()
        {
            GenericPlayer p = CreateTestingPlayer();
            p.Winner();
            return true;
        }
        private static bool TestGenericPlayer0GetScore()
        {
            Requires(TestGenericPlayer0Winner);

            GenericPlayer p = CreateTestingPlayer();
            int wins = p.GetScore();
            p.Winner();
            if (p.GetScore() == wins + 1) return true;
            return false;
        }
        private static bool TestPlayerController0BeginRound()
        {
            GenericPlayer p = CreateTestingPlayer();
            p.BeginRound();
            return true;
        }
        private static bool TestPlayerController0NewTurn()
        {
            Requires(TestBattle0NewGame);
            Requires(TestBattle0GetPlayerNumber);
            Battle game = InitialiseGame();

            game.NewGame();

            // Find the gameplay form
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Battle.NewGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in GameplayForm");
                return false;
            }

            // Disable the control panel to check that NewTurn enables it
            controlPanel.Enabled = false;

            game.GetPlayerNumber(1).NewTurn(gameplayForm, game);

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after HumanPlayer.NewTurn()");
                return false;
            }
            return true;

        }
        private static bool TestPlayerController0ReportHit()
        {
            GenericPlayer p = CreateTestingPlayer();
            p.ReportHit(0, 0);
            return true;
        }

        private static bool TestGameplayTank0GameplayTank()
        {
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            return true;
        }
        private static bool TestGameplayTank0GetPlayerNumber()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            if (playerTank.GetPlayerNumber() == p) return true;
            return false;
        }
        private static bool TestGameplayTank0GetTank()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGenericPlayer0GetTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            if (playerTank.GetTank() == playerTank.GetPlayerNumber().GetTank()) return true;
            return false;
        }
        private static bool TestGameplayTank0GetTankAngle()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            float angle = playerTank.GetTankAngle();
            if (angle >= -90 && angle <= 90) return true;
            return false;
        }
        private static bool TestGameplayTank0SetAngle()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0GetTankAngle);
            float angle = 75;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.SetAngle(angle);
            if (FloatEquals(playerTank.GetTankAngle(), angle)) return true;
            return false;
        }
        private static bool TestGameplayTank0GetTankPower()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);

            playerTank.GetTankPower();
            return true;
        }
        private static bool TestGameplayTank0SetForce()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0GetTankPower);
            int power = 65;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.SetForce(power);
            if (playerTank.GetTankPower() == power) return true;
            return false;
        }
        private static bool TestGameplayTank0GetCurrentWeapon()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);

            playerTank.GetCurrentWeapon();
            return true;
        }
        private static bool TestGameplayTank0SelectWeapon()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0GetCurrentWeapon);
            int weapon = 3;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.SelectWeapon(weapon);
            if (playerTank.GetCurrentWeapon() == weapon) return true;
            return false;
        }
        private static bool TestGameplayTank0Render()
        {
            Requires(TestGameplayTank0GameplayTank);
            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.Render(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestGameplayTank0XPos()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, x, y, game);
            if (playerTank.XPos() == x) return true;
            return false;
        }
        private static bool TestGameplayTank0GetY()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, x, y, game);
            if (playerTank.GetY() == y) return true;
            return false;
        }
        private static bool TestGameplayTank0Launch()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.Launch();
            return true;
        }
        private static bool TestGameplayTank0DamagePlayer()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();

            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.DamagePlayer(10);
            return true;
        }
        private static bool TestGameplayTank0TankExists()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0DamagePlayer);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            if (!playerTank.TankExists()) return false;
            playerTank.DamagePlayer(playerTank.GetTank().GetTankArmour());
            if (playerTank.TankExists()) return false;
            return true;
        }
        private static bool TestGameplayTank0ProcessGravity()
        {
            Requires(TestBattle0GetMap);
            Requires(TestBattlefield0TerrainDestruction);
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0DamagePlayer);
            Requires(TestGameplayTank0TankExists);
            Requires(TestGameplayTank0GetTank);
            Requires(TestChassis0GetTankArmour);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            // Unfortunately we need to rely on DestroyTerrain() to get rid of any terrain that may be in the way
            game.GetMap().TerrainDestruction(Battlefield.WIDTH / 2.0f, Battlefield.HEIGHT / 2.0f, 20);
            GameplayTank playerTank = new GameplayTank(p, Battlefield.WIDTH / 2, Battlefield.HEIGHT / 2, game);
            int oldX = playerTank.XPos();
            int oldY = playerTank.GetY();

            playerTank.ProcessGravity();

            if (playerTank.XPos() != oldX)
            {
                SetErrorDescription("Caused X coordinate to change.");
                return false;
            }
            if (playerTank.GetY() != oldY + 1)
            {
                SetErrorDescription("Did not cause Y coordinate to increase by 1.");
                return false;
            }

            int initialArmour = playerTank.GetTank().GetTankArmour();
            // The tank should have lost 1 armour from falling 1 tile already, so do
            // (initialArmour - 2) damage to the tank then drop it again. That should kill it.

            if (!playerTank.TankExists())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.DamagePlayer(initialArmour - 2);
            if (!playerTank.TankExists())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.ProcessGravity();
            if (playerTank.TankExists())
            {
                SetErrorDescription("Tank survived despite taking enough falling damage to destroy it");
                return false;
            }

            return true;
        }
        private static bool TestBattlefield0Battlefield()
        {
            Battlefield battlefield = new Battlefield();
            return true;
        }
        private static bool TestBattlefield0IsTileAt()
        {
            Requires(TestBattlefield0Battlefield);

            bool foundTrue = false;
            bool foundFalse = false;
            Battlefield battlefield = new Battlefield();
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.IsTileAt(x, y))
                    {
                        foundTrue = true;
                    }
                    else
                    {
                        foundFalse = true;
                    }
                }
            }

            if (!foundTrue)
            {
                SetErrorDescription("IsTileAt() did not return true for any tile.");
                return false;
            }

            if (!foundFalse)
            {
                SetErrorDescription("IsTileAt() did not return false for any tile.");
                return false;
            }

            return true;
        }
        private static bool TestBattlefield0TankFits()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);

            Battlefield battlefield = new Battlefield();
            for (int y = 0; y <= Battlefield.HEIGHT - Chassis.HEIGHT; y++)
            {
                for (int x = 0; x <= Battlefield.WIDTH - Chassis.WIDTH; x++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < Chassis.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < Chassis.WIDTH; ix++)
                        {

                            if (battlefield.IsTileAt(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        if (battlefield.TankFits(x, y))
                        {
                            SetErrorDescription("Found collision where there shouldn't be one");
                            return false;
                        }
                    }
                    else
                    {
                        if (!battlefield.TankFits(x, y))
                        {
                            SetErrorDescription("Didn't find collision where there should be one");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private static bool TestBattlefield0TankVerticalPosition()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);

            Battlefield battlefield = new Battlefield();
            for (int x = 0; x <= Battlefield.WIDTH - Chassis.WIDTH; x++)
            {
                int lowestValid = 0;
                for (int y = 0; y <= Battlefield.HEIGHT - Chassis.HEIGHT; y++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < Chassis.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < Chassis.WIDTH; ix++)
                        {

                            if (battlefield.IsTileAt(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        lowestValid = y;
                    }
                }

                int placedY = battlefield.TankVerticalPosition(x);
                if (placedY != lowestValid)
                {
                    SetErrorDescription(string.Format("Tank was placed at {0},{1} when it should have been placed at {0},{2}", x, placedY, lowestValid));
                    return false;
                }
            }
            return true;
        }
        private static bool TestBattlefield0TerrainDestruction()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);

            Battlefield battlefield = new Battlefield();
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.IsTileAt(x, y))
                    {
                        battlefield.TerrainDestruction(x, y, 0.5f);
                        if (battlefield.IsTileAt(x, y))
                        {
                            SetErrorDescription("Attempted to destroy terrain but it still exists");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            SetErrorDescription("Did not find any terrain to destroy");
            return false;
        }
        private static bool TestBattlefield0ProcessGravity()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);
            Requires(TestBattlefield0TerrainDestruction);

            Battlefield battlefield = new Battlefield();
            for (int x = 0; x < Battlefield.WIDTH; x++)
            {
                if (battlefield.IsTileAt(x, Battlefield.HEIGHT - 1))
                {
                    if (battlefield.IsTileAt(x, Battlefield.HEIGHT - 2))
                    {
                        // Seek up and find the first non-set tile
                        for (int y = Battlefield.HEIGHT - 2; y >= 0; y--)
                        {
                            if (!battlefield.IsTileAt(x, y))
                            {
                                // Do a gravity step and make sure it doesn't slip down
                                battlefield.ProcessGravity();
                                if (!battlefield.IsTileAt(x, y + 1))
                                {
                                    SetErrorDescription("Moved down terrain even though there was no room");
                                    return false;
                                }

                                // Destroy the bottom-most tile
                                battlefield.TerrainDestruction(x, Battlefield.HEIGHT - 1, 0.5f);

                                // Do a gravity step and make sure it does slip down
                                battlefield.ProcessGravity();

                                if (battlefield.IsTileAt(x, y + 1))
                                {
                                    SetErrorDescription("Terrain didn't fall");
                                    return false;
                                }

                                // Otherwise this seems to have worked
                                return true;
                            }
                        }


                    }
                }
            }
            SetErrorDescription("Did not find any appropriate terrain to test");
            return false;
        }
        private static bool TestWeaponEffect0SetCurrentGame()
        {
            Requires(TestBoom0Boom);
            Requires(TestBattle0Battle);

            WeaponEffect weaponEffect = new Boom(1, 1, 1);
            Battle game = new Battle(2, 1);
            weaponEffect.SetCurrentGame(game);
            return true;
        }
        private static bool TestShell0Shell()
        {
            Requires(TestBoom0Boom);
            GenericPlayer player = CreateTestingPlayer();
            Boom explosion = new Boom(1, 1, 1);
            Shell projectile = new Shell(25, 25, 45, 30, 0.02f, explosion, player);
            return true;
        }
        private static bool TestShell0Step()
        {
            Requires(TestBattle0NewGame);
            Requires(TestBoom0Boom);
            Requires(TestShell0Shell);
            Requires(TestWeaponEffect0SetCurrentGame);
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Boom explosion = new Boom(1, 1, 1);

            Shell projectile = new Shell(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.SetCurrentGame(game);
            projectile.Step();

            // We can't really test this one without a substantial framework,
            // so we just call it and hope that everything works out

            return true;
        }
        private static bool TestShell0Render()
        {
            Requires(TestBattle0NewGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBoom0Boom);
            Requires(TestShell0Shell);
            Requires(TestWeaponEffect0SetCurrentGame);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the projectile
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Boom explosion = new Boom(1, 1, 1);

            Shell projectile = new Shell(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.SetCurrentGame(game);
            projectile.Render(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestBoom0Boom()
        {
            GenericPlayer player = CreateTestingPlayer();
            Boom explosion = new Boom(1, 1, 1);

            return true;
        }
        private static bool TestBoom0Detonate()
        {
            Requires(TestBoom0Boom);
            Requires(TestWeaponEffect0SetCurrentGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);

            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Boom explosion = new Boom(1, 1, 1);
            explosion.SetCurrentGame(game);
            explosion.Detonate(25, 25);

            return true;
        }
        private static bool TestBoom0Step()
        {
            Requires(TestBoom0Boom);
            Requires(TestWeaponEffect0SetCurrentGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);
            Requires(TestBoom0Detonate);

            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Boom explosion = new Boom(1, 1, 1);
            explosion.SetCurrentGame(game);
            explosion.Detonate(25, 25);
            explosion.Step();

            // Again, we can't really test this one without a full framework

            return true;
        }
        private static bool TestBoom0Render()
        {
            Requires(TestBoom0Boom);
            Requires(TestWeaponEffect0SetCurrentGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);
            Requires(TestBoom0Detonate);
            Requires(TestBoom0Step);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the explosion
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Boom explosion = new Boom(10, 10, 10);
            explosion.SetCurrentGame(game);
            explosion.Detonate(25, 25);
            // Step it for a bit so we can be sure the explosion is visible
            for (int i = 0; i < 10; i++)
            {
                explosion.Step();
            }
            explosion.Render(graphics, bitmapSize);

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }

        private static GameplayForm InitialiseGameplayForm(out NumericUpDown angleCtrl, out TrackBar powerCtrl, out Button fireCtrl, out Panel controlPanel, out ListBox weaponSelect)
        {
            Requires(TestBattle0NewGame);

            Battle game = InitialiseGame();

            angleCtrl = null;
            powerCtrl = null;
            fireCtrl = null;
            controlPanel = null;
            weaponSelect = null;

            game.NewGame();
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Battle.NewGame() did not create a GameplayForm and that is the only way GameplayForm can be tested");
                return null;
            }

            bool foundDisplayPanel = false;
            bool foundControlPanel = false;

            foreach (Control c in gameplayForm.Controls)
            {
                // The only controls should be 2 panels
                if (c is Panel)
                {
                    // Is this the control panel or the display panel?
                    Panel p = c as Panel;

                    // The display panel will have 0 controls.
                    // The control panel will have separate, of which only a few are mandatory
                    int controlsFound = 0;
                    bool foundFire = false;
                    bool foundAngle = false;
                    bool foundAngleLabel = false;
                    bool foundPower = false;
                    bool foundPowerLabel = false;


                    foreach (Control pc in p.Controls)
                    {
                        controlsFound++;

                        // Mandatory controls for the control panel are:
                        // A 'Fire!' button
                        // A NumericUpDown for controlling the angle
                        // A TrackBar for controlling the power
                        // "Power:" and "Angle:" labels

                        if (pc is Label)
                        {
                            Label lbl = pc as Label;
                            if (lbl.Text.ToLower().Contains("angle"))
                            {
                                foundAngleLabel = true;
                            }
                            else
                            if (lbl.Text.ToLower().Contains("power"))
                            {
                                foundPowerLabel = true;
                            }
                        }
                        else
                        if (pc is Button)
                        {
                            Button btn = pc as Button;
                            if (btn.Text.ToLower().Contains("fire"))
                            {
                                foundFire = true;
                                fireCtrl = btn;
                            }
                        }
                        else
                        if (pc is TrackBar)
                        {
                            foundPower = true;
                            powerCtrl = pc as TrackBar;
                        }
                        else
                        if (pc is NumericUpDown)
                        {
                            foundAngle = true;
                            angleCtrl = pc as NumericUpDown;
                        }
                        else
                        if (pc is ListBox)
                        {
                            weaponSelect = pc as ListBox;
                        }
                    }

                    if (controlsFound == 0)
                    {
                        foundDisplayPanel = true;
                    }
                    else
                    {
                        if (!foundFire)
                        {
                            SetErrorDescription("Control panel lacks a \"Fire!\" button OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngle)
                        {
                            SetErrorDescription("Control panel lacks an angle NumericUpDown OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPower)
                        {
                            SetErrorDescription("Control panel lacks a power TrackBar OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngleLabel)
                        {
                            SetErrorDescription("Control panel lacks an \"Angle:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPowerLabel)
                        {
                            SetErrorDescription("Control panel lacks a \"Power:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }

                        foundControlPanel = true;
                        controlPanel = p;
                    }

                }
                else
                {
                    SetErrorDescription(string.Format("Unexpected control ({0}) named \"{1}\" found in GameplayForm", c.GetType().FullName, c.Name));
                    return null;
                }
            }

            if (!foundDisplayPanel)
            {
                SetErrorDescription("No display panel found");
                return null;
            }
            if (!foundControlPanel)
            {
                SetErrorDescription("No control panel found");
                return null;
            }
            return gameplayForm;
        }

        private static bool TestGameplayForm0GameplayForm()
        {
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            return true;
        }
        private static bool TestGameplayForm0EnableTankControls()
        {
            Requires(TestGameplayForm0GameplayForm);
            Battle game = InitialiseGame();
            game.NewGame();

            // Find the gameplay form
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Battle.NewGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in GameplayForm");
                return false;
            }

            // Disable the control panel to check that EnableControlPanel enables it
            controlPanel.Enabled = false;

            gameplayForm.EnableTankControls();

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after GameplayForm.EnableTankControls()");
                return false;
            }
            return true;

        }
        private static bool TestGameplayForm0SetAngle()
        {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            float testAngle = 27;

            gameplayForm.SetAngle(testAngle);
            if (FloatEquals((float)angle.Value, testAngle)) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set angle to {0} but angle is {1}", testAngle, (float)angle.Value));
                return false;
            }
        }
        private static bool TestGameplayForm0SetForce()
        {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            int testPower = 71;

            gameplayForm.SetForce(testPower);
            if (power.Value == testPower) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set power to {0} but power is {1}", testPower, power.Value));
                return false;
            }
        }
        private static bool TestGameplayForm0SelectWeapon()
        {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            gameplayForm.SelectWeapon(0);

            // WeaponSelect is optional behaviour, so it's okay if it's not implemented here, as long as the method works.
            return true;
        }
        private static bool TestGameplayForm0Launch()
        {
            Requires(TestGameplayForm0GameplayForm);
            // This is something we can't really test properly without a proper framework, so for now we'll just click
            // the button and make sure it disables the control panel
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            controlPanel.Enabled = true;
            fire.PerformClick();
            if (controlPanel.Enabled)
            {
                SetErrorDescription("Control panel still enabled immediately after clicking fire button");
                return false;
            }

            return true;
        }
        private static void UnitTests()
        {
            DoTest(TestBattle0Battle);
            DoTest(TestBattle0NumPlayers);
            DoTest(TestBattle0GetRounds);
            DoTest(TestBattle0RegisterPlayer);
            DoTest(TestBattle0GetPlayerNumber);
            DoTest(TestBattle0PlayerColour);
            DoTest(TestBattle0CalculatePlayerPositions);
            DoTest(TestBattle0Rearrange);
            DoTest(TestBattle0NewGame);
            DoTest(TestBattle0GetMap);
            DoTest(TestBattle0GetCurrentGameplayTank);
            DoTest(TestChassis0GetTank);
            DoTest(TestChassis0DisplayTank);
            DoTest(TestChassis0SetLine);
            DoTest(TestChassis0GetTankArmour);
            DoTest(TestChassis0ListWeapons);
            DoTest(TestGenericPlayer0PlayerController);
            DoTest(TestGenericPlayer0GetTank);
            DoTest(TestGenericPlayer0PlayerName);
            DoTest(TestGenericPlayer0GetColour);
            DoTest(TestGenericPlayer0Winner);
            DoTest(TestGenericPlayer0GetScore);
            DoTest(TestPlayerController0BeginRound);
            DoTest(TestPlayerController0NewTurn);
            DoTest(TestPlayerController0ReportHit);
            DoTest(TestGameplayTank0GameplayTank);
            DoTest(TestGameplayTank0GetPlayerNumber);
            DoTest(TestGameplayTank0GetTank);
            DoTest(TestGameplayTank0GetTankAngle);
            DoTest(TestGameplayTank0SetAngle);
            DoTest(TestGameplayTank0GetTankPower);
            DoTest(TestGameplayTank0SetForce);
            DoTest(TestGameplayTank0GetCurrentWeapon);
            DoTest(TestGameplayTank0SelectWeapon);
            DoTest(TestGameplayTank0Render);
            DoTest(TestGameplayTank0XPos);
            DoTest(TestGameplayTank0GetY);
            DoTest(TestGameplayTank0Launch);
            DoTest(TestGameplayTank0DamagePlayer);
            DoTest(TestGameplayTank0TankExists);
            DoTest(TestGameplayTank0ProcessGravity);
            DoTest(TestBattlefield0Battlefield);
            DoTest(TestBattlefield0IsTileAt);
            DoTest(TestBattlefield0TankFits);
            DoTest(TestBattlefield0TankVerticalPosition);
            DoTest(TestBattlefield0TerrainDestruction);
            DoTest(TestBattlefield0ProcessGravity);
            DoTest(TestWeaponEffect0SetCurrentGame);
            DoTest(TestShell0Shell);
            DoTest(TestShell0Step);
            DoTest(TestShell0Render);
            DoTest(TestBoom0Boom);
            DoTest(TestBoom0Detonate);
            DoTest(TestBoom0Step);
            DoTest(TestBoom0Render);
            DoTest(TestGameplayForm0GameplayForm);
            DoTest(TestGameplayForm0EnableTankControls);
            DoTest(TestGameplayForm0SetAngle);
            DoTest(TestGameplayForm0SetForce);
            DoTest(TestGameplayForm0SelectWeapon);
            DoTest(TestGameplayForm0Launch);
        }
        
        #endregion
        
        #region CheckClasses

        private static bool CheckClasses()
        {
            string[] classNames = new string[] { "Program", "ComputerPlayer", "Battlefield", "Boom", "GameplayForm", "Battle", "PlayerController", "Shell", "GenericPlayer", "GameplayTank", "Chassis", "WeaponEffect" };
            string[][] classFields = new string[][] {
                new string[] { "Main" }, // Program
                new string[] { }, // ComputerPlayer
                new string[] { "IsTileAt","TankFits","TankVerticalPosition","TerrainDestruction","ProcessGravity","WIDTH","HEIGHT"}, // Battlefield
                new string[] { "Detonate" }, // Boom
                new string[] { "EnableTankControls","SetAngle","SetForce","SelectWeapon","Launch","InitDisplayBuffer"}, // GameplayForm
                new string[] { "NumPlayers","GetCurrentRound","GetRounds","RegisterPlayer","GetPlayerNumber","GetGameplayTank","PlayerColour","CalculatePlayerPositions","Rearrange","NewGame","CommenceRound","GetMap","DrawTanks","GetCurrentGameplayTank","AddWeaponEffect","ProcessWeaponEffects","DisplayEffects","CancelEffect","CheckCollidedTank","DamagePlayer","ProcessGravity","TurnOver","FindWinner","NextRound","WindSpeed"}, // Battle
                new string[] { }, // PlayerController
                new string[] { }, // Shell
                new string[] { "GetTank","PlayerName","GetColour","Winner","GetScore","BeginRound","NewTurn","ReportHit"}, // GenericPlayer
                new string[] { "GetPlayerNumber","GetTank","GetTankAngle","SetAngle","GetTankPower","SetForce","GetCurrentWeapon","SelectWeapon","Render","XPos","GetY","Launch","DamagePlayer","TankExists","ProcessGravity"}, // GameplayTank
                new string[] { "DisplayTank","SetLine","CreateTankBMP","GetTankArmour","ListWeapons","ShootWeapon","GetTank","WIDTH","HEIGHT","NUM_TANKS"}, // Chassis
                new string[] { "SetCurrentGame","Step","Render"} // WeaponEffect
            };

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("Checking classes for public methods...");
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    if (type.Namespace != "TankBattle")
                    {
                        Console.WriteLine("Public type {0} is not in the TankBattle namespace.", type.FullName);
                        return false;
                    }
                    else
                    {
                        int typeIdx = -1;
                        for (int i = 0; i < classNames.Length; i++)
                        {
                            if (type.Name == classNames[i])
                            {
                                typeIdx = i;
                                classNames[typeIdx] = null;
                                break;
                            }
                        }
                        foreach (MemberInfo memberInfo in type.GetMembers())
                        {
                            string memberName = memberInfo.Name;
                            bool isInherited = false;
                            foreach (MemberInfo parentMemberInfo in type.BaseType.GetMembers())
                            {
                                if (memberInfo.Name == parentMemberInfo.Name)
                                {
                                    isInherited = true;
                                    break;
                                }
                            }
                            if (!isInherited)
                            {
                                if (typeIdx != -1)
                                {
                                    bool fieldFound = false;
                                    if (memberName[0] != '.')
                                    {
                                        foreach (string allowedFields in classFields[typeIdx])
                                        {
                                            if (memberName == allowedFields)
                                            {
                                                fieldFound = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fieldFound = true;
                                    }
                                    if (!fieldFound)
                                    {
                                        Console.WriteLine("The public field \"{0}\" is not one of the authorised fields for the {1} class.\n", memberName, type.Name);
                                        Console.WriteLine("Remove it or change its access level.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    //Console.WriteLine("{0} passed.", type.FullName);
                }
            }
            for (int i = 0; i < classNames.Length; i++)
            {
                if (classNames[i] != null)
                {
                    Console.WriteLine("The class \"{0}\" is missing.", classNames[i]);
                    return false;
                }
            }
            Console.WriteLine("All public methods okay.");
            return true;
        }
        
        #endregion

        public static void Main()
        {
            if (CheckClasses())
            {
                UnitTests();

                int passed = 0;
                int failed = 0;
                foreach (string key in unitTestResults.Keys)
                {
                    if (unitTestResults[key] == "Passed")
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                }

                //begin test
                /*
                Console.WriteLine("A");
                Battlefield b = new Battlefield();
                b.TerrainDestruction(50, 20, 10);
                b.TerrainDestruction(0, 119, 1);
                b.TerrainDestruction(5, 119, 1);
                b.TerrainDestruction(159, 119, 1);
                b.TerrainDestruction(0, 118, 1);
                b.TerrainDestruction(5, 118, 1);
                b.TerrainDestruction(159, 118, 1);
                for (int height = 0; height < Battlefield.HEIGHT; height++)
                {
                    for (int width = 0; width < Battlefield.WIDTH; width++)
                    {
                        if (b.IsTileAt(width, height))
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                }
                
                    b.ProcessGravity();
              
                
                for (int height = 0; height < Battlefield.HEIGHT; height++)
                {
                    for (int width = 0; width < Battlefield.WIDTH; width++)
                    {
                        if(b.IsTileAt(width, height))
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                }

                if (b.TankFits(159, 0))
                {
                    Console.Write("true");
                }*/
                float angle = 75;
                GenericPlayer p = CreateTestingPlayer();
                Battle game = InitialiseGame();
                GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
                playerTank.SetAngle(angle);
                //end test
                Console.WriteLine("\n{0}/{1} unit tests passed", passed, passed + failed);
                if (failed == 0)
                {
                    Console.WriteLine("Starting up TankBattle...");
                    Program.Main();
                    return;
                }
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
