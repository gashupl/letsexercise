export class GoalsMonthlyChart {
    public async execute(): Promise<void>
    {
        console.log("GoalsMonthlyChart.execute()");

        // Access the Xrm context
        const XrmContext = (window as any).parent.Xrm || (window as any).top.Xrm;

        if (XrmContext) {
            console.log("Xrm context is available");
            const userId = XrmContext.Utility.getGlobalContext().userSettings.userId;

            let currentDate = new Date();
            let startMonth = this.getMonthInYYYYMM(new Date(currentDate.getFullYear(), currentDate.getMonth() - 12, 1));
            let endMonth = this.getMonthInYYYYMM(currentDate);

            var results = await this.getMonthlyResults(XrmContext, startMonth, endMonth);
            console.log(results); 
            //[{"MonthCodeName":"2024-09","MonthFriendlyName":"September","Result":0},{"MonthCodeName":"2024-10","MonthFriendlyName":"October","Result":0},{"MonthCodeName":"2024-11","MonthFriendlyName":"November","Result":0},{"MonthCodeName":"2024-12","MonthFriendlyName":"December","Result":0},{"MonthCodeName":"2025-01","MonthFriendlyName":"January","Result":0},{"MonthCodeName":"2025-02","MonthFriendlyName":"February","Result":0},{"MonthCodeName":"2025-03","MonthFriendlyName":"March","Result":0},{"MonthCodeName":"2025-04","MonthFriendlyName":"April","Result":0},{"MonthCodeName":"2025-05","MonthFriendlyName":"May","Result":0},{"MonthCodeName":"2025-06","MonthFriendlyName":"June","Result":0},{"MonthCodeName":"2025-07","MonthFriendlyName":"July","Result":0},{"MonthCodeName":"2025-08","MonthFriendlyName":"August","Result":0},{"MonthCodeName":"2025-09","MonthFriendlyName":"September","Result":33}]
           // return; 

        } else {
            console.log("Xrm context is not available");
        }
    }


 private async getMonthlyResults(XrmContext: any, startMonth: string, endMonth: string): Promise<void>{
    const execute_pg_monthlyresults_Request = {
        startmonth: startMonth,
        endmonth: endMonth,
        getMetadata: function () {
            return {
                boundParameter: null,
                parameterTypes: {
                    startmonth: { typeName: "Edm.String", structuralProperty: 1 },
                    endmonth: { typeName: "Edm.String", structuralProperty: 1 }
                },
                operationType: 1,
                operationName: "pg_monthlyresults"
            };
        }
    };

    try {
        const response = await XrmContext.WebApi.execute(execute_pg_monthlyresults_Request);
        if (response.ok) {
            const responseBody = await response.json();
            return responseBody["results"]; // Edm.String
            // You can process results here
        } else {
            console.error(`Web API call failed: ${response.statusText}`);
        }
    } catch (error: any) {
        console.error("Error executing pg_monthlyresults:", error.message || error);
    }
}

    private getMonthInYYYYMM(date: Date): string {
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        return `${year}-${month}`;
    }

}