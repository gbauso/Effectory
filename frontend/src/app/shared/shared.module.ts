import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProblemComponent } from './components/problem/problem.component';
import { SharedRoutingModule } from './shared-routing.module';
import { CardComponent } from './components/card/card.component';

@NgModule({
  declarations: [NavbarComponent, ProblemComponent, CardComponent],
  imports: [CommonModule, SharedRoutingModule],
  exports: [NavbarComponent, ProblemComponent, CardComponent],
})
export class SharedModule {}
