import { CommonLib } from '../Common'

export class ExerciseRecordService {

    common: CommonLib
    context: Xrm.Events.EventContext

    constructor(context: Xrm.Events.EventContext) {
        this.common = new CommonLib(context)
        this.context = context; 
    }

    public setDateOnNewForm() : void
    {
        if (this.context.getFormContext().ui.getFormType() === 1){
            let dateAttribute =  this.context.getFormContext().getAttribute("pg_date"); 
            let date = new Date(); 
            dateAttribute.setValue(date);
        }
    }
}