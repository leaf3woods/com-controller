using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Domain.Excel
{
    public class ExcelLoader
    {
        public static IEnumerable<ApnExcelField> LoadCardInfoFromExcel(Stream fileStream, string extension, string path)
        {
            var items = path.Split(':', 2);
            var sheetName = items[0];
            var columns = items[1].Split(',', 4);
            var userName = columns[0];
            var userId = columns[1];
            var iccid = columns[2];
            var l_imsi = columns[3];

            IWorkbook workbook = extension switch
            {
                ".xls" => new HSSFWorkbook(fileStream),
                ".xlsx" => new XSSFWorkbook(fileStream),
                _ => throw new Exception("unsupported file format")
            };
            var sheet = (workbook ??
                throw new Exception("read file failed")).GetSheet(sheetName) ??
                throw new Exception($"sheet:{sheetName} not exist");
            using var headers = sheet.GetRow(0).GetEnumerator();
            var index = (-1, -1, -1, -1);
            while (headers.MoveNext())
            {
                var cell = headers.Current ?? throw new Exception("null cell");
                if (cell.StringCellValue == userName)
                    index.Item1 = cell.ColumnIndex;
                if (cell.StringCellValue == userId)
                    index.Item2 = cell.ColumnIndex;
                if (cell.StringCellValue == iccid)
                    index.Item3 = cell.ColumnIndex;
                if (cell.StringCellValue == l_imsi)
                    index.Item4 = cell.ColumnIndex;
                if (index.Item1 >= 0 && index.Item2 >= 0 && index.Item3 >= 0 && index.Item4 >= 0)
                    break;
            }

            if (index.Item1 < 0 || index.Item2 < 0 || index.Item3 < 0 || index.Item4 < 0)
            {
                throw new Exception("can't find suitable column");
            }

            var raws = sheet.GetEnumerator();
            while (raws.MoveNext())
            {
                var raw = raws.Current as IRow;
                if (raw!.RowNum == 0 || raw.Cells.Count < 2) continue;
                yield return new ApnExcelField()
                {
                    Name = raw!.Cells[index.Item1].StringCellValue,
                    UserId = raw!.Cells[index.Item2].StringCellValue,
                    ICCID = raw!.Cells[index.Item3].StringCellValue,
                    LIMSI = raw!.Cells[index.Item4].StringCellValue
                };
                //else
            }
        }
    }
}