namespace Charts

module FakeCharts =
    open XPlot.GoogleCharts

    let statusCount () =

        let series = [("Open",23); ("In Progress",5); ("Resolved", 58); ("Closed",5)]

        let options =
            Options(
                title = "Status", 
                orientation = "horizontal")

        if series |> Seq.isEmpty then "no data" else
            let chart = 
                series
                |> Chart.Bar
                |> Chart.WithOptions options
                |> Chart.WithLabels ["Count"]

            chart.GetInlineHtml()

    let performance() =
        let data = ["Noten", 80; "Anwesenheit", 75; "Disziplin", 20]
        let options =
            Options(
                width = 600,
                height = 200,
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

