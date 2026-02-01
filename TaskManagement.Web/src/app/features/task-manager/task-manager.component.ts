import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { TaskManager } from '../task-manager/task-manager-model/task-manager.model';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { ReferenceData } from '../../shared/models/reference-data.model';
import { DataService } from '../../core/services/data.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CreateTaskComponent } from '../task-manager/create-task/create-task.component';

@Component({
  selector: 'app-task-manager',
  templateUrl: './task-manager.component.html',
  styleUrl: './task-manager.component.css'
})
export class TaskManagerComponent implements OnInit, AfterViewInit {

  public taskTypeList: any[] = [];
  public tasksList: any[] = [];
  public taskSelected: any;
  @ViewChild(CreateTaskComponent) createTask!: CreateTaskComponent;  
  displayedColumns: string[] = ['name', 'taskType', "startDate", "endDate", "actions"];
  dataSource = new MatTableDataSource(this.tasksList);
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private route: ActivatedRoute, private dataService: DataService, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.loadTasksGrid();
    this.taskTypeList = this.route.snapshot.data['referenceData'] as ReferenceData[];
    this.getReferenceData();
  }

  ngAfterViewInit() {
    this.loadTasksGrid()
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(row: any) {
    this.taskSelected = row;
  }


  getReferenceData() {
    this.dataService.getAllByGet<any>("/TaskType").subscribe({
      next: res => {
        this.taskTypeList = res as ReferenceData[];
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
          this.createTask.resetForm();
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
  }
}
