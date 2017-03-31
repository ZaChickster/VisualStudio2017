import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { ToDo } from '../../models/interfaces';

@Component({
    selector: 'todo',
    templateUrl: './todo.component.html',
    styleUrls: ['./todo.component.css']
})

export class ToDoComponent {
    model: ToDo[];
    filtered: ToDo[];

    _ajax: Http;
    _showCompleted = true;

    constructor(http: Http, route: ActivatedRoute) {
        this._ajax = http;
        this.getPageData();
    }

    getPageData() {
        var url = '/api/ToDos';

        this._ajax.get(url).subscribe(result => {
            this.model = result.json() as ToDo[];
            this.toggleCompleted();
        });
    }

    toggleCompleted() {
        this.filtered = [];
        this.filtered = this.model.filter(todo => todo.completed === this._showCompleted || todo.completed === false);
        this._showCompleted = !this._showCompleted;
    }
}