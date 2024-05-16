import { ExerciseRecordService } from '../services/ExerciseRecordService'

export class ExerciseRecord{

    public static async onLoad(context : Xrm.Events.EventContext) {
        const accountService = new ExerciseRecordService(context)
        accountService.setDateOnNewForm(); 
    }
}
