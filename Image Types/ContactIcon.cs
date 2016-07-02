using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace iFruitAddon
{
    public sealed class ContactIcon : PhoneImage
    {
        /// <summary>
        /// Initialize the class.
        /// </summary>
        /// <param name="txd"></param>
        public ContactIcon(string txd) : base(txd)
        { }

        public static ContactIcon Generic { get { return new ContactIcon("char_default"); } }
        public static ContactIcon Abigail { get { return new ContactIcon("char_abigail"); } }
        public static ContactIcon AllCharacters { get { return new ContactIcon("char_all_players_conf"); } }
        public static ContactIcon Amanda { get { return new ContactIcon("char_amanda"); } }
        public static ContactIcon Ammunation { get { return new ContactIcon("char_ammunation"); } }
        public static ContactIcon Andreas { get { return new ContactIcon("char_andreas"); } }
        public static ContactIcon Antonia { get { return new ContactIcon("char_antonia"); } }
        public static ContactIcon Arthur { get { return new ContactIcon("char_arthur"); } }
        public static ContactIcon Ashley { get { return new ContactIcon("char_ashley"); } }
        public static ContactIcon BankOfLiberty { get { return new ContactIcon("char_bank_bol"); } }
        public static ContactIcon FleecaBank { get { return new ContactIcon("char_bank_fleeca"); } }
        public static ContactIcon MazeBank { get { return new ContactIcon("char_bank_maze"); } }
        public static ContactIcon Barry { get { return new ContactIcon("char_barry"); } }
        public static ContactIcon Beverly { get { return new ContactIcon("char_beverly"); } }
        public static ContactIcon BikeSite { get { return new ContactIcon("char_bikesite"); } }
        public static ContactIcon Blank { get { return new ContactIcon("char_blank_entry"); } }
        public static ContactIcon Blimp { get { return new ContactIcon("char_blimp"); } }
        public static ContactIcon Blocked { get { return new ContactIcon("char_blocked"); } }
        public static ContactIcon BoatSite { get { return new ContactIcon("char_boatsite"); } }
        public static ContactIcon BrokenDownGirl { get { return new ContactIcon("char_broken_down_girl"); } }
        public static ContactIcon Bugstars { get { return new ContactIcon("char_bugstars"); } }
        public static ContactIcon Emergency { get { return new ContactIcon("char_call911"); } }
        public static ContactIcon LegendaryMotorsport { get { return new ContactIcon("char_carsite"); } }
        public static ContactIcon SSASuperAutos { get { return new ContactIcon("char_carsite2"); } }
        public static ContactIcon Castro { get { return new ContactIcon("char_castro"); } }
        public static ContactIcon ChatIcon { get { return new ContactIcon("char_chat_call"); } }
        public static ContactIcon Chef { get { return new ContactIcon("char_chef"); } }
        public static ContactIcon Cheng { get { return new ContactIcon("char_cheng"); } }
        public static ContactIcon ChengSr { get { return new ContactIcon("char_chengsr"); } }
        public static ContactIcon Chop { get { return new ContactIcon("char_chop"); } }
        public static ContactIcon CreatorPortraits { get { return new ContactIcon("char_creator_portraits"); } }
        public static ContactIcon Cris { get { return new ContactIcon("char_cris"); } }
        public static ContactIcon Dave { get { return new ContactIcon("char_dave"); } }
        public static ContactIcon Denise { get { return new ContactIcon("char_denise"); } }
        public static ContactIcon DetonateBomb { get { return new ContactIcon("char_detonatebomb"); } }
        public static ContactIcon DetonatePhone { get { return new ContactIcon("char_detonatephone"); } }
        public static ContactIcon Devin { get { return new ContactIcon("char_devin"); } }
        public static ContactIcon DialASub { get { return new ContactIcon("char_dial_a_sub"); } }
        public static ContactIcon Dom { get { return new ContactIcon("char_dom"); } }
        public static ContactIcon DomesticGirl { get { return new ContactIcon("char_domestic_girl"); } }
        public static ContactIcon Dreyfuss { get { return new ContactIcon("char_dreyfuss"); } }
        public static ContactIcon DrFriedlander { get { return new ContactIcon("char_dr_friedlander"); } }
        public static ContactIcon Epsilon { get { return new ContactIcon("char_epsilon"); } }
        public static ContactIcon EstateAgent { get { return new ContactIcon("char_estate_agent"); } }
        public static ContactIcon Facebook { get { return new ContactIcon("char_facebook"); } }
        public static ContactIcon Zombie { get { return new ContactIcon("char_filmnoir"); } }
        public static ContactIcon Floyd { get { return new ContactIcon("char_floyd"); } }
        public static ContactIcon Franklin { get { return new ContactIcon("char_franklin"); } }
        public static ContactIcon FranklinTrevor { get { return new ContactIcon("char_frank_trev_conf"); } }
        public static ContactIcon GayMilitary { get { return new ContactIcon("char_gaymilitary"); } }
        public static ContactIcon Hao { get { return new ContactIcon("char_hao"); } }
        public static ContactIcon HitcherGirl { get { return new ContactIcon("char_hitcher_girl"); } }
        public static ContactIcon Human { get { return new ContactIcon("char_humandefault"); } }
        public static ContactIcon Hunter { get { return new ContactIcon("char_hunter"); } }
        public static ContactIcon Jimmy { get { return new ContactIcon("char_jimmy"); } }
        public static ContactIcon JimmyBoston { get { return new ContactIcon("char_jimmy_boston"); } }
        public static ContactIcon Joe { get { return new ContactIcon("char_joe"); } }
        public static ContactIcon JoseF { get { return new ContactIcon("char_josef"); } }
        public static ContactIcon Josh { get { return new ContactIcon("char_josh"); } }
        public static ContactIcon Lamar { get { return new ContactIcon("char_lamar"); } }
        public static ContactIcon Lazlow { get { return new ContactIcon("char_lazlow"); } }
        public static ContactIcon Lester { get { return new ContactIcon("char_lazlow"); } }
        public static ContactIcon Skull { get { return new ContactIcon("char_lester_deathwish"); } }
        public static ContactIcon LesterFranklin { get { return new ContactIcon("char_lest_frank_conf"); } }
        public static ContactIcon LesterMike { get { return new ContactIcon("char_lester_mike_conf"); } }
        public static ContactIcon LifeInvader { get { return new ContactIcon("char_lifeinvader"); } }
        public static ContactIcon LSCustoms { get { return new ContactIcon("char_ls_customs"); } }
        public static ContactIcon Sasquatch { get { return new ContactIcon("char_sasquatch"); } }
        public static ContactIcon StripperChastity { get { return new ContactIcon("char_stripper_chastity"); } }
        public static ContactIcon StripperCheetah { get { return new ContactIcon("char_stripper_cheetah"); } }
        public static ContactIcon StripperFufu { get { return new ContactIcon("char_stripper_fufu"); } }
        public static ContactIcon StripperInfernus { get { return new ContactIcon("char_stripper_infernus"); } }
        public static ContactIcon StripperJuliet { get { return new ContactIcon("char_stripper_juliet"); } }
        public static ContactIcon StripperNikki { get { return new ContactIcon("char_stripper_nikki"); } }
        public static ContactIcon StripperPeach { get { return new ContactIcon("char_stripper_peach"); } }
        public static ContactIcon StripperSapphire { get { return new ContactIcon("char_stripper_sapphire"); } }
        public static ContactIcon Taxi { get { return new ContactIcon("char_taxi"); } }
        public static ContactIcon Trevor { get { return new ContactIcon("char_trevor"); } }
        public static ContactIcon Wade { get { return new ContactIcon("char_wade"); } }
        public static ContactIcon Army { get { return new ContactIcon("dia_army"); } }
        public static ContactIcon Brad { get { return new ContactIcon("dia_brad"); } }
        public static ContactIcon Dealer { get { return new ContactIcon("dia_dealer"); } }
        public static ContactIcon Driver { get { return new ContactIcon("dia_driver"); } }
        public static ContactIcon LoveFist { get { return new ContactIcon("dia_lovefist"); } }
        public static ContactIcon Pilot { get { return new ContactIcon("dia_pilot"); } }
        public static ContactIcon Police { get { return new ContactIcon("dia_police"); } }
        public static ContactIcon Victim { get { return new ContactIcon("dia_victim"); } }
        public static ContactIcon Target { get { return new ContactIcon("dia_target"); } }
        public static ContactIcon Globe { get { return new ContactIcon("rank_globe_21x21"); } }
        public static ContactIcon Pegasus { get { return new ContactIcon("char_pegasus_delivery"); } }
        public static ContactIcon Bennys { get { return new ContactIcon("char_carsite3"); } }
        public static ContactIcon PlaneSite { get { return new ContactIcon("char_planesite"); } }
        public static ContactIcon MilSite { get { return new ContactIcon("char_milsite"); } }
        public static ContactIcon Captain { get { return new ContactIcon("char_boatsite2"); } }
        public static ContactIcon ActingUp { get { return new ContactIcon("char_acting_up"); } }
        public static ContactIcon Blimp2 { get { return new ContactIcon("char_blimp2"); } }
        public static ContactIcon GangApp { get { return new ContactIcon("char_gangapp"); } }
        public static ContactIcon AgentHaines { get { return new ContactIcon("dia_agent14"); } }
        public static ContactIcon Maxim { get { return new ContactIcon("dia_maxim"); } }
    }
}
