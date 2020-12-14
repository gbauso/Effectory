import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProblemComponent } from './shared/components/problem/problem.component';

const routes: Routes = [ {
  path: 'error',
  component: ProblemComponent,
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
