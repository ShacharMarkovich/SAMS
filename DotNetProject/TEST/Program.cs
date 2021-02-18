namespace DriveQuickstart
{
    class Program
    {
        public static void Main()
        {
            //DAL.GoogleDriveAPI.GetAllData();
            BL.DataHandle.GenerateQRcodes();

            System.Collections.Generic.List<BE.Order> orders = BL.DataHandle.parseBitmapList(BL.DataHandle.loadQRBitmaps());
            var bl = new BL.DataHandle();
            foreach (var order  in orders)
                bl.AddOrder(order);
        }
    }
}
