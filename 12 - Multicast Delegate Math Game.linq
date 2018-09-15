<Query Kind="Program" />

/*
# Math Game
http://www.umassmed.edu/bsrc/tricks/#days1998

## Instructions

1. Pick the number of days a week that you would like to go out (1 - 7).
2. Multiply this number by 2.
3. Add 5.
4. Multiply the new total by 50.
5. In 1998, if you have already had your birthday this year, add 1748. If not, add 1747. In 1999,
   just add 1 to these two numbers (so add 1749 if you already had your birthday, and add 1748 if
   you haven't). In 2000, the number change to 1749 and 1748. And so on.
6. Subtract the four digit year that you were born (19XX).

## Results:

* You should have a three-digit number.
* The first digit of this number was the number of days you want to go out each week (1-7).
* The last two digits are your age.
*/

class GameSetup
{
    public GameSetup(int daysOut, DateTime birthDate)
    {
        DaysOut = daysOut;
        BirthDate = birthDate;
    }
    
    public int DaysOut { get; }
    public DateTime BirthDate { get; }
}

class GameResult
{
    public GameResult(int result)
    {
        Result = result;
        
        var parts =
            result
                .ToString()
                .Map(s => Tuple.Create(s.Substring(0, 1).Map(int.Parse), s.Substring(1, 2).Map(int.Parse)));
        DaysOut = parts.Item1;
        Age = parts.Item2;
    }

    public int Result { get; }
    public int DaysOut { get; }
    public int Age { get; }
}

int GetYearAdjustment(DateTime today, DateTime birthdate) {
	var hadBirthday =
		today.CompareTo(new DateTime(today.Year, birthdate.Month, birthdate.Day)) >= 0;

	return 1748 + (today.Year - 1998) - (hadBirthday ? 0 : -1);
}

GameResult Play(GameSetup setup) =>
    DelegateHelper
        .Combine(
            new Func<int, int>[] {
                x => x,
                x => x * 2,
                x => x + 5,
                x => x * 50,
                x => x + GetYearAdjustment(DateTime.Now, setup.BirthDate),
				x => x - setup.BirthDate.Year
            })
        .InvokeChain(setup.DaysOut)
        .Map(r => new GameResult(r));

void Main() 
{
    new GameSetup(3, DateTime.Parse("1979-12-19"))
        .Dump()
        .Map(Play)
        .Dump();
}