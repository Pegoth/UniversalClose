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
                    }
                },
                data.SoundEventPath
            );
        }

        public static void Postfix()
        {
            UniversalCloseModule.IsInquaryVisible = true;
        }
    }

    [HarmonyPatch(typeof(InformationManager), "HideInquiry")]
    internal class InformationManagerHideInquiryPostfix
    {
        public static void Postfix()
        {
            UniversalCloseModule.IsInquaryVisible = false;
        }
    }

    [HarmonyPatch(typeof(InformationManager), "ClearAllMessages")]
    internal class InformationManagerClearAllMessagesPostfix
    {
        public static void Postfix()
        {
            UniversalCloseModule.IsInquaryVisible = false;
        }
    }
}