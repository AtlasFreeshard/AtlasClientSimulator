﻿using System;
using System.Threading;
using DOL.Database.UniqueID;

namespace AtlasSimulator.playerclasses
{
    public class Wizard : IPlayerClass
    {
        public string accountName { get; }
        public string charname { get; }
        public string password { get; }
        public string sql { get; }
        private Client _client;
        private Timer _actionTimer;

        public Wizard(string acc, string pw, string cn, GLocation initialGLocation)
        {
            // Assign
            accountName = acc;
            password = pw;
            charname = cn;
            
            // setup client
            _client = new Client(acc, pw, cn);

            // handle adding to the database
            ClassHelpers.ClassData wizzyData = ClassHelpers.GetWizardData();

            sql = String.Format(
                "INSERT INTO `atlas`.`dolcharacters` (`Constitution`, `Dexterity`, `Strength`, `Quickness`, `Intelligence`," +
                " `Piety`, `Empathy`, `Charisma`, `MaxEndurance`, `Endurance`, `Concentration`, `AccountName`, `AccountSlot`," +
                "`Name`, `Race`, `Level`, `Class`, `Realm`, `CreationModel`, `CurrentModel`, `Region`, `Xpos`," +
                " `Ypos`, `Zpos`, `BindXpos`, `BindYpos`, `BindZpos`, `BindRegion`, `MaxSpeed`, `SerializedSpecs`, `DOLCharacters_ID`)" +
                " VALUES('400', '400', '400', '400', '1000', '400', '400', '400', '100', '100', '100'," +
                " '{0}', '1', '{1}', '{2}', '50', '{3}', '{4}', '{5}', '{5}', '{6}', '{7}'," +
                " '{8}', '{9}','{7}', '{8}', '{9}', '{5}', '191', '{10}', '{11}');",
                accountName, charname, wizzyData._validRaces[0], wizzyData._classID, wizzyData._realm, wizzyData._creationModel,
                initialGLocation.zone, initialGLocation.x,
                initialGLocation.y, initialGLocation.z, wizzyData._specString, IDGenerator.GenerateID());
            Console.WriteLine(sql);
            
            // setup actions
            _actionTimer = new Timer(ActionCallback, null, 850, 1000);

        }
        public void login()
        {
            _client.Login();
        }

        public void ActionCallback(Object source)
        {
            // Cast PBAOE
            _client.SendUseSpell(0, 48, 5);
        }
    }
}