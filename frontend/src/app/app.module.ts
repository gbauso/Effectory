import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { SurveyModule } from './survey/survey.module';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { ProblemComponent } from './shared/components/problem/problem.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProblemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    SurveyModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
