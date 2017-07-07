import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { ToDoComponent } from './components/todo/todo.component';
import { ToDoDetailsComponent } from './components/tododetails/tododetails.component';
import { MongoComponent } from './components/mongo/mongo.component';
import { MongoDetailsComponent } from './components/mongodetails/mongodetails.component';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        ToDoComponent,
        ToDoDetailsComponent,
        MongoComponent,
        MongoDetailsComponent,
        HomeComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'todo/details', component: ToDoDetailsComponent },
            { path: 'todo', component: ToDoComponent },
            { path: 'todo/details/:id', component: ToDoDetailsComponent },
            { path: 'mongo/details', component: MongoDetailsComponent },
            { path: 'mongo', component: MongoComponent },
            { path: 'mongo/:page', component: MongoComponent },
            { path: 'mongo/details/:id', component: MongoDetailsComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        FormsModule
    ]
})
export class AppModule {
}
