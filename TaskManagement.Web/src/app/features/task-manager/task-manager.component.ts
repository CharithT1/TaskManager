import { Component, OnInit, ViewChild } from '@angular/core';
import { TaskManager } from '../task-manager/task-manager-model/task-manager.model';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { ReferenceData } from '../../shared/models/reference-data.model';
import { DataService } from '../../core/services/data.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-task-manager',
  templateUrl: './task-manager.component.html',
  styleUrl: './task-manager.component.css'
})
export class TaskManagerComponent implements OnInit {

  public taskTypeList: ReferenceData[] = [];
  public tasksList: TaskManager[] = [];
  public taskSelected: TaskManager = new TaskManager();
  displayedColumns: string[] = ['name', 'taskType', "startDate", "endDate", "actions"];
  dataSource = new MatTableDataSource(this.tasksList);
  @ViewChild(MatSort) sort!: MatSort;
  public createTaskForm: FormGroup = new FormGroup({});

  constructor(private route: ActivatedRoute, private dataService: DataService, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.loadTasksGrid();
    this.createForm();
    this.taskTypeList = this.route.snapshot.data['referenceData'] as ReferenceData[];
    this.getReferenceData();
  }

  createForm() {
    this.createTaskForm = new FormGroup({
      id: new FormControl<number>(this.taskSelected.id),
      name: new FormControl<string>('', [Validators.required, Validators.maxLength(50)]),
      description: new FormControl<string>('', [Validators.maxLength(1000)]),
      taskTypeId: new FormControl<number | ''>('',Validators.required),
      startDate: new FormControl<Date | null>(null, [Validators.required]),
      endDate: new FormControl<Date | null>(null, [Validators.required])
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(row: TaskManager) {
    this.taskSelected = row;
    this.bindFormData();
  }

  bindFormData() {
    this.createTaskForm.get('id')?.patchValue(this.taskSelected.id);
    this.createTaskForm.get('name')?.patchValue(this.taskSelected.name);
    this.createTaskForm.get('description')?.patchValue(this.taskSelected.description);
    this.createTaskForm.get('taskTypeId')?.patchValue(this.taskSelected.taskTypeId);
    this.createTaskForm.get('startDate')?.patchValue(this.taskSelected.startDate);
    this.createTaskForm.get('endDate')?.patchValue(this.taskSelected.endDate);
  }

  getReferenceData() {
    this.dataService.getAllByGet<ReferenceData[]>("/TaskType").subscribe({
      next: res => {
        this.taskTypeList = res;
      },
      error: err => { console.log(err); }
    });

  }

  populateForm(record: TaskManager) {
    this.taskSelected = record;
  }

  deleteTask(task: any) {
    if (confirm("Are you sure your want to delete the task?")) {
      this.dataService.delete<any>("/ManageTask", task.id).subscribe({
        next: res => {
          this.loadTasksGrid();
          this.toastr.error("Deleted Succesffully!", "Task Manager");       
          this.taskSelected= new TaskManager();
          this.bindFormData();
        },
        error: err => { console.log(err); }
      });
    }
  }

  loadTasksGrid() {
    this.dataService.getAllByGet("/ManageTask").subscribe({
      next: res => {
        this.tasksList = res as TaskManager[];
        this.dataSource.data = this.tasksList;
        this.dataSource.sort = this.sort;       
      },
      error: err => { console.log(err); }
    });
  }

  onTaskSaveSuccess() {
    this.loadTasksGrid();
         this.taskSelected= new TaskManager();
          this.bindFormData();
  }
}
