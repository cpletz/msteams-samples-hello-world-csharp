namespace Charts

module FakeCharts =
    open XPlot.GoogleCharts
    open System

    let defaultChartWidth = 600
    let rnd = Random()

    let performance() =
        let data = ["Noten", 80; "Anwesenheit", 75; "Disziplin", 20]
        let options =
            Options(
                width = defaultChartWidth,
                redFrom = 0,
                redTo = 20,
                yellowFrom = 20,
                yellowTo = 40,
                greenFrom = 80,
                greenTo = 100,
                minorTicks = 5
            )
        let chart = 
            Chart.Gauge data
            |> Chart.WithOptions options

        chart.GetInlineHtml()

    let someDate inFuture =     
        let minOffset, maxOffset = if inFuture then 1, 75 else -75, -1 
        let offset = rnd.Next(minOffset, maxOffset) |> float
        let hour = (8 + rnd.Next(0, 5)) |> float
        DateTime.Now.Date.AddDays(offset).AddHours(hour)

    let someDateInThePast() = someDate false
    let someDateInTheFuture() = someDate true

    let marks() = 
        let data = [
            "M", someDateInThePast(), 6, "Mathematik"
            "M", someDateInThePast(), 5, "Mathematik"
            "M", someDateInThePast(), 6, "Mathematik"
            "B", someDateInThePast(), 4, "Biologie"
            "B", someDateInThePast(), 5, "Biologie"
            "B", someDateInThePast(), 4, "Biologie"
            "E", someDateInThePast(), 3, "Englisch"
            "E", someDateInThePast(), 4, "Englisch"
            "E", someDateInThePast(), 2, "Englisch"
        ]

        let va = new Axis(minValue = 0, maxValue = 7, ticks = [|1;2;3;4;5;6|])

        let options =
            Options(
                vAxis = va,
                height = 300,
                width = defaultChartWidth * 2)

        let chart = 
            data
            |> Chart.Bubble
            |> Chart.WithOptions options
            |> Chart.WithLabels ["Datum"; "Note"; "Fach"]

        chart.GetInlineHtml()

    let nextTests() = 
        let data = [
            "M", someDateInTheFuture(), 2, "Mathematik"
            "M", someDateInTheFuture(), 2, "Mathematik"
            "M", someDateInTheFuture(), 2, "Mathematik"
            "B", someDateInTheFuture(), 2, "Biologie"
            "B", someDateInTheFuture(), 2, "Biologie"
            "B", someDateInTheFuture(), 2, "Biologie"
            "E", someDateInTheFuture(), 2, "Englisch"
            "E", someDateInTheFuture(), 2, "Englisch"
            "E", someDateInTheFuture(), 2, "Englisch"
        ]

        let va = new Axis(minValue = 0, maxValue = 3, ticks = [||])

        let options =
            Options(
                vAxis = va,
                height = 200,
                width = defaultChartWidth * 2)

        let chart = 
            data
            |> Chart.Bubble
            |> Chart.WithOptions options
            |> Chart.WithLabels ["Datum"; "D"; "Fach"]

        chart.GetInlineHtml()

    let optimalTestDates() = 

        let vacation = [
            DateTime(2018, 12, 24), DateTime(2019,1,5)
            DateTime(2019, 4, 15), DateTime(2019,4,27)
            DateTime(2019, 7, 22), DateTime(2019,8,10)
            DateTime(2019, 9, 30), DateTime(2019,10,12)
            DateTime(2019, 12, 24), DateTime(2020,1,5)

            ]

        let isFree (date : DateTime) = 
            if date.DayOfWeek = DayOfWeek.Saturday || date.DayOfWeek = DayOfWeek.Sunday then
                false
            else
                vacation |> List.exists (fun (s, e) -> date >= s && date < e)

        let weekDayFactor (date : DateTime) =
             match date.DayOfWeek with
             | DayOfWeek.Monday -> 0.8
             | DayOfWeek.Friday -> 0.7
             | _ -> 0.99
         
        let winterFactor (date : DateTime) = 
            let winter = [11; 12; 1; 2]
            let isWinter = winter |> List.contains date.Month
            if isWinter then 0.9
            else 1.0

        let getFactor date =
            let value = (weekDayFactor date) * (winterFactor date) * (0.5 + rnd.NextDouble()/2.0)
            Math.Round(value, 2)

        let today = DateTime.Now.Date
        let daysTillEndOf2019 = (DateTime(2019,12,31) - today).Days



        let data = 
            [1..daysTillEndOf2019]
            |> List.map (fun o -> today.AddDays (o |> float))
            |> List.filter (fun d -> not(isFree d))
            |> List.map (fun d -> d, getFactor d)
            |> List.toSeq
            
        let options =
            Options(height = 350)

        let chart = 
            data
            |> Chart.Calendar
            |> Chart.WithOptions options

        chart.GetInlineHtml()
