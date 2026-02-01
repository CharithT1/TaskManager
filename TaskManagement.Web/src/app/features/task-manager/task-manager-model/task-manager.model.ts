import { ReferenceData } from "../../../shared/models/reference-data.model"

export class TaskManager {
    id: number = 0
    name: string = ""
    description: string = ""
    taskTypeId: number = 0
    taskType: ReferenceData =new ReferenceData()
    startDate: Date = new Date()
    endDate: Date = new Date()
}

