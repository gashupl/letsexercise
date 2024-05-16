/// <reference path="../node_modules/@types/xrm/index.d.ts" />
declare namespace Pg_exercisegoalEnum {
    const enum pg_exercise {
        PushUps = 1,
    }

    const enum pg_goaltype {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
    }

    const enum statuscode {
        Active = 1,
        Inactive = 2,
    }

    const enum statecode {
        Active = 0,
        Inactive = 1,
    }

}

declare namespace Xrm {
    type Pg_exercisegoal = Omit<FormContext, 'getAttribute'> & Omit<FormContext, 'getControl'> & Pg_exercisegoalAttributes;

    interface EventContext {
        getFormContext(): Pg_exercisegoal;
    }

    interface Pg_exercisegoalAttributes {
        getAttribute(name: "createdby"): Attributes.LookupAttribute;
        getAttribute(name: "createdbyname"): Attributes.StringAttribute;
        getAttribute(name: "createdbyyominame"): Attributes.StringAttribute;
        getAttribute(name: "createdon"): Attributes.DateAttribute;
        getAttribute(name: "createdonbehalfby"): Attributes.LookupAttribute;
        getAttribute(name: "createdonbehalfbyname"): Attributes.StringAttribute;
        getAttribute(name: "createdonbehalfbyyominame"): Attributes.StringAttribute;
        getAttribute(name: "importsequencenumber"): Attributes.NumberAttribute;
        getAttribute(name: "modifiedby"): Attributes.LookupAttribute;
        getAttribute(name: "modifiedbyname"): Attributes.StringAttribute;
        getAttribute(name: "modifiedbyyominame"): Attributes.StringAttribute;
        getAttribute(name: "modifiedon"): Attributes.DateAttribute;
        getAttribute(name: "modifiedonbehalfby"): Attributes.LookupAttribute;
        getAttribute(name: "modifiedonbehalfbyname"): Attributes.StringAttribute;
        getAttribute(name: "modifiedonbehalfbyyominame"): Attributes.StringAttribute;
        getAttribute(name: "overriddencreatedon"): Attributes.DateAttribute;
        getAttribute(name: "ownerid"): Attributes.LookupAttribute;
        getAttribute(name: "owneridname"): Attributes.StringAttribute;
        getAttribute(name: "owneridtype"): Attributes.Attribute;
        getAttribute(name: "owneridyominame"): Attributes.StringAttribute;
        getAttribute(name: "owningbusinessunit"): Attributes.LookupAttribute;
        getAttribute(name: "owningbusinessunitname"): Attributes.StringAttribute;
        getAttribute(name: "owningteam"): Attributes.LookupAttribute;
        getAttribute(name: "owninguser"): Attributes.LookupAttribute;
        getAttribute(name: "pg_completedpercentage"): Attributes.NumberAttribute;
        getAttribute(name: "pg_exercise"): Attributes.OptionSetAttribute;
        getAttribute(name: "pg_exercisegoalid"): Attributes.StringAttribute;
        getAttribute(name: "pg_goaltype"): Attributes.OptionSetAttribute;
        getAttribute(name: "pg_name"): Attributes.StringAttribute;
        getAttribute(name: "pg_scorenumber"): Attributes.NumberAttribute;
        getAttribute(name: "statecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "statuscode"): Attributes.OptionSetAttribute;
        getAttribute(name: "timezoneruleversionnumber"): Attributes.NumberAttribute;
        getAttribute(name: "utcconversiontimezonecode"): Attributes.NumberAttribute;
        getAttribute(name: "versionnumber"): Attributes.NumberAttribute;
        getControl(name: "createdby"): Controls.LookupControl;
        getControl(name: "createdbyname"): Controls.StringControl;
        getControl(name: "createdbyyominame"): Controls.StringControl;
        getControl(name: "createdon"): Controls.DateControl;
        getControl(name: "createdonbehalfby"): Controls.LookupControl;
        getControl(name: "createdonbehalfbyname"): Controls.StringControl;
        getControl(name: "createdonbehalfbyyominame"): Controls.StringControl;
        getControl(name: "importsequencenumber"): Controls.NumberControl;
        getControl(name: "modifiedby"): Controls.LookupControl;
        getControl(name: "modifiedbyname"): Controls.StringControl;
        getControl(name: "modifiedbyyominame"): Controls.StringControl;
        getControl(name: "modifiedon"): Controls.DateControl;
        getControl(name: "modifiedonbehalfby"): Controls.LookupControl;
        getControl(name: "modifiedonbehalfbyname"): Controls.StringControl;
        getControl(name: "modifiedonbehalfbyyominame"): Controls.StringControl;
        getControl(name: "overriddencreatedon"): Controls.DateControl;
        getControl(name: "ownerid"): Controls.LookupControl;
        getControl(name: "owneridname"): Controls.StringControl;
        getControl(name: "owneridtype"): Controls.Control;
        getControl(name: "owneridyominame"): Controls.StringControl;
        getControl(name: "owningbusinessunit"): Controls.LookupControl;
        getControl(name: "owningbusinessunitname"): Controls.StringControl;
        getControl(name: "owningteam"): Controls.LookupControl;
        getControl(name: "owninguser"): Controls.LookupControl;
        getControl(name: "pg_completedpercentage"): Controls.NumberControl;
        getControl(name: "pg_exercise"): Controls.OptionSetControl;
        getControl(name: "pg_exercisegoalid"): Controls.StringControl;
        getControl(name: "pg_goaltype"): Controls.OptionSetControl;
        getControl(name: "pg_name"): Controls.StringControl;
        getControl(name: "pg_scorenumber"): Controls.NumberControl;
        getControl(name: "statecode"): Controls.OptionSetControl;
        getControl(name: "statuscode"): Controls.OptionSetControl;
        getControl(name: "timezoneruleversionnumber"): Controls.NumberControl;
        getControl(name: "utcconversiontimezonecode"): Controls.NumberControl;
        getControl(name: "versionnumber"): Controls.NumberControl;
    }

}

