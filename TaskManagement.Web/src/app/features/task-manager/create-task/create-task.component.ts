import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DataService } from '../../../core/services/data.service';
import { ReferenceData } from '../../../shared/models/reference-data.model';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent {
  @Input() taskTypeListList !: ReferenceData[];
  @Output() taskSaveSuccess = new EventEmitter<void>();
  @Input() createTaskForm!: FormGroup;

  constructor(private dataService: DataService, private toastr: ToastrService) {

  }

  onSubmit() {
    if (this.createTaskForm.valid) {
      const formValue = this.createTaskForm.getRawValue();
      if (formValue.id == 0) {
        this.insertForm(formValue);
      }
      else
        this.updateForm(formValue);
    }
  }

  insertForm(form: any) {
    this.dataService
      .add<any>("/ManageTask", form)
      .subscribe({
        next: res => this.callSuccess(res, form),
        error: err => { console.log(err); }
      });
  }

  updateForm(form: any) {
    this.dataService
      .put<any>("/ManageTask", form).subscribe({
        next: res => this.callSuccess(res, form),
        error: err => { console.log(err); }
      });
  }

  callSuccess(result: any, form: any) {
    if (result != null) {
      if (!result.isError) {
        this.toastr.success(`${form.id > 0 ? 'Updated' : 'Inserted'} Succesffully!`, "Task Manager");
        this.taskSaveSuccess.emit();
      } else {
        this.toastr.error(result.errors[0], "Error : Task Manager");
      }
    }
  }
}
