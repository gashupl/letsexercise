export class GoalsMonthlyChart {
    public execute() : void
    {
        console.log("GoalsMonthlyChart.execute()");

        // Access the Xrm context
        const XrmContext = (window as any).parent.Xrm || (window as any).top.Xrm;

        if (XrmContext) {
            console.log("Xrm context is available");
            // You can now use the Xrm context
            // Example: Get the current user ID
            const userId = XrmContext.Utility.getGlobalContext().userSettings.userId;
            console.log("Current User ID:", userId);
        } else {
            console.log("Xrm context is not available");
        }
    }
}