
using org.in2bits.MyXls;
using System.Collections.Generic;
public class TestInfo
{
	public int dressType;
	public int iconID;
	public string model;
	public  string  modelPos;
};
public class ExcelMakerManager
{ 
	public static ExcelMakerManager eInstance;
	public static ExcelMakerManager CreateExcelMakerManager()
	{
		if (eInstance == null)
		{
			eInstance = new ExcelMakerManager();
		}
		return eInstance;
	}
	public void ExcelMaker(string name, List<TestInfo> listInfo)
	{
		XlsDocument xls = new XlsDocument();
		xls.FileName = name;
		xls.SummaryInformation.Author = "xyy";
		xls.SummaryInformation.Subject = "test";

		string sheetName = "Sheet0";
		Worksheet sheet = xls.Workbook.Worksheets.AddNamed(sheetName);
		Cells cells = sheet.Cells;

		int rowNum = listInfo.Count;
		int rowMin = 1;
		int row = 0;

		for (int x = 0; x < rowNum + 1; x++)
		{
			if (x == 0)
			{
				cells.Add(1, 1, "服装类型");
				cells.Add(1, 2, "图片索引");
				cells.Add(1, 3, "对应模型");
				cells.Add(1, 4, "模型位置");
			}
			else
			{
				cells.Add(rowMin + x, 1, listInfo[row].dressType);
				cells.Add(rowMin + x, 2, listInfo[row].iconID);
				cells.Add(rowMin + x, 3, listInfo[row].model);
				cells.Add(rowMin + x, 4, listInfo[row].modelPos);
				row++;
			}
		}
		xls.Save();
	}
}