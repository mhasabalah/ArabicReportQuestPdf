
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

static class SimpleExtension
{
    private static IContainer Cell(this IContainer container, bool dark)
    {
        return container
            .Border(1)
            .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
            .Padding(10);
    }

    public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();
    public static IContainer ValueCell(this IContainer container) => container.Cell(false);
    public static void ArabicNumerals(this IContainer container, int number, bool isBold = false)
    {
        if (isBold)
        {
            container.Text(number.ToString().Replace('0', '\u0660')
                                            .Replace('1', '\u0661')
                                            .Replace('2', '\u0662')
                                            .Replace('3', '\u0663')
                                            .Replace('4', '\u0664')
                                            .Replace('5', '\u0665')
                                            .Replace('6', '\u0666')
                                            .Replace('7', '\u0667')
                                            .Replace('8', '\u0668')
                                            .Replace('9', '\u0669'))
                                            .Bold()
                                            .DirectionFromLeftToRight();
        }
        else
        {
            container.Text(number.ToString().Replace('0', '\u0660')
                                            .Replace('1', '\u0661')
                                            .Replace('2', '\u0662')
                                            .Replace('3', '\u0663')
                                            .Replace('4', '\u0664')
                                            .Replace('5', '\u0665')
                                            .Replace('6', '\u0666')
                                            .Replace('7', '\u0667')
                                            .Replace('8', '\u0668')
                                            .Replace('9', '\u0669'))
                                            .DirectionFromLeftToRight();
        }
    }

    //function to make date in arabic format
    public static TextSpanDescriptor ArabicDate(this IContainer container, DateTime date, string format = "yyyy/MM/dd")
    {
        return
        container.Text(date.ToString(format).Replace('0', '\u0660')
                                            .Replace('1', '\u0661')
                                            .Replace('2', '\u0662')
                                            .Replace('3', '\u0663')
                                            .Replace('4', '\u0664')
                                            .Replace('5', '\u0665')
                                            .Replace('6', '\u0666')
                                            .Replace('7', '\u0667')
                                            .Replace('8', '\u0668')
                                            .Replace('9', '\u0669'))
                                            .DirectionFromLeftToRight();
    }



}