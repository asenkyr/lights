namespace lights.PrettyPrinters
{
    public static class GenericPrettyPrinter
    {
        public static string CheckBox(bool state)
        {
            return state ? "[x]" : "[ ]";
        }
    }
}
