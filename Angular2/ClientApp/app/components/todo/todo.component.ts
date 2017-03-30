import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { ToDo } from '../../models/interfaces';

@Component({
    selector: 'todo',
    templateUrl: './todo.component.html'
})

export class ToDoComponent {
    ajax: Http;

    public model: ToDo[];

    constructor(http: Http, route: ActivatedRoute) {
        var me = this;
        me.ajax = http;

        me.getPageData();
    }

    getPageData() {
        var url = '/api/ToDos';

        this.ajax.get(url).subscribe(result => {
            this.model = result.json() as ToDo[];
        });

        return false;
    }
}