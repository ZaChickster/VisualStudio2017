import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { MongoComponent } from './components/mongo/mongo.component';
import { MongoDetailsComponent } from './components/mongodetails/mongodetails.component';
import { ToDoComponent } from './components/todo/todo.component';
import { ToDoDetailsComponent } from './components/tododetails/tododetails.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        MongoComponent,
        MongoDetailsComponent,
        ToDoComponent,
        ToDoDetailsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'mongo', component: MongoComponent },
            { path: 'mongo/:page', component: MongoComponent },
            { path: 'mongo/details/:id', component: MongoDetailsComponent },
            { path: 'todo/details', component: ToDoDetailsComponent },
            { path: 'todo', component: ToDoComponent },
            { path: 'todo/details/:id', component: ToDoDetailsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
