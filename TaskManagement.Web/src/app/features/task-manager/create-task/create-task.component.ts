import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DataService } from '../../../core/services/data.service';


@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent {
  @Input() taskTypeListList: any;
  @Output() taskSaveSuccess = new EventEmitter<void>();
  @Input() createTaskForm!: FormGroup;

  constructor(public dataService: DataService, private toastr: ToastrService) {

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
        next: res => {
          if (res != null) {
            if (!res.isError) {
              this.resetForm();
              this.toastr.success("Inserted Succesffully!", "Task Manager");
              this.taskSaveSuccess.emit();
            } else {
              this.toastr.error(res.errors[0], "Error : Task Manager");
            }
          }
        },
        error: err => { console.log(err); }
      });
  }

  updateForm(form: any) {
    this.dataService
      .put<any>("/ManageTask", form).subscribe({
        next: res => {
          if (res != null) {
            if (!res.isError) {
              this.resetForm();
              this.toastr.success("Updated Succesffully!", "Task Manager");
              this.taskSaveSuccess.emit();
            } else {
               this.toastr.error(res.errors[0], "Error : Task Manager");
            }
          }
        },
        error: err => { console.log(err); }
      });
  }
  resetForm() {
    this.createTaskForm.reset();
  }
}
