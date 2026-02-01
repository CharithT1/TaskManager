import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskManagerComponent } from './features/task-manager/task-manager.component';
import { ReferenceDataResolver } from './core/resolvers/reference-data-resolver';

const routes: Routes = [
  {
    path: '',
    component: TaskManagerComponent,
    resolve: {
      referenceData: ReferenceDataResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
