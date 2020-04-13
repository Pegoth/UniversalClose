using HarmonyLib;
using TaleWorlds.Core;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(InformationManager), "ShowInquiry")]
    internal class InformationManagerShowInquiryPrefixPostfix
    {
        public static void Prefix(ref InquiryData data)
        {
            var origAff = data.AffirmativeAction;
            var origNeg = data.NegativeAction;

            data = new InquiryData(
                data.TitleText,
                data.Text,
                data.IsAffirmativeOptionShown,
                data.IsNegativeOptionShown,
                data.AffirmativeText,
                data.NegativeText,
                () =>
                {
                    try
                    {
                        origAff?.Invoke();
                    }
                    finally
                    {
                        UniversalCloseModule.IsInquaryVisible = true;
                        DebugLogger.Print("InquiryData.AffirmativeAction called");
                    }
                },
                () =>
                {
                    try
                    {
                        origNeg?.Invoke();
                    }
                    finally
                    {
                        UniversalCloseModule.IsInquaryVisible = false;
                        DebugLogger.Print("InquiryData.NegativeAction called");
                    }
                },
                data.SoundEventPath
            );
        }

        public static void Postfix()
        {
            UniversalCloseModule.IsInquaryVisible = true;
            DebugLogger.Print("InformationManager.ShowInquiry called");
        }
    }

    [HarmonyPatch(typeof(InformationManager), "HideInquiry")]
    internal class InformationManagerHideInquiryPostfix
    {
        public static void Postfix()
        {
            UniversalCloseModule.IsInquaryVisible = false;
            DebugLogger.Print("InformationManager.HideInquiry called");
        }
    }

    [HarmonyPatch(typeof(InformationManager), "ClearAllMessages")]
    internal class InformationManagerClearAllMessagesPostfix
    {
        public static void Postfix()
        {
            UniversalCloseModule.IsInquaryVisible = false;
            DebugLogger.Print("InformationManager.ClearAllMessages called");
        }
    }
}