import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DataService } from '../../shared/services/data.service';


@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent implements OnInit, OnChanges {
  @Input() taskTypeListList: any;
  @Input() taskSelected: any;
  @Output() taskSaveSuccess = new EventEmitter<void>();


  id: Number = 0;
  public createTaskForm: UntypedFormGroup = new UntypedFormGroup({});

  constructor(public dataService: DataService, private toastr: ToastrService) {

  }
  ngOnInit(): void {
    this.createForm();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['taskSelected']) {
      this.bindFormData();
    }
  }

  bindFormData() {
    this.createTaskForm.get('id')?.patchValue(this.taskSelected.id);
    this.createTaskForm.get('name')?.patchValue(this.taskSelected.name);
    this.createTaskForm.get('description')?.patchValue(this.taskSelected.description);
    this.createTaskForm.get('taskTypeId')?.patchValue(this.taskSelected.taskTypeId);
    this.createTaskForm.get('startDate')?.patchValue(this.taskSelected.startDate);
    this.createTaskForm.get('endDate')?.patchValue(this.taskSelected.endDate);
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

  createForm() {
    this.createTaskForm = new UntypedFormGroup({
      id: new UntypedFormControl(this.id),
      name: new UntypedFormControl('', [Validators.required, Validators.maxLength(50)]),
      description: new UntypedFormControl('', [Validators.maxLength(1000)]),
      taskTypeId: new UntypedFormControl('', Validators.required),
      startDate: new UntypedFormControl(null, [Validators.required]),
      endDate: new UntypedFormControl(null, [Validators.required])
    });
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
