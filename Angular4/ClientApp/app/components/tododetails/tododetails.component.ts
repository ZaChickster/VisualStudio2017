import { Component } from '@angular/core';
import { Http, Request, RequestOptions, RequestMethod, Headers } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ToDo } from '../../models/interfaces';

@Component({
    selector: 'tododetails',
    templateUrl: './tododetails.component.html',
    styleUrls: ['./tododetails.component.css']
})
export class ToDoDetailsComponent {
    model: ToDoDetailModel;

    _ajax: Http;
    _route: ActivatedRoute;
    _url: string;

    constructor(http: Http, route: ActivatedRoute, location: Location) {
        var me = this;

        me.model = {} as ToDoDetailModel;
        me._ajax = http;
        me._route = route;
        me._url = '/api/ToDo';

        route.params.subscribe(params => {
            me.updateModel(params['id'])
        });
    }

    updateModel(id: string) {
        var url = '';

        if (id) {
            url = this._url + '?id=' + id;

            this._ajax.get(url).subscribe(result => {
                this.model.data = result.json() as ToDo;
            });
        } else {
            this.model.data = {} as ToDo;
        }        
    }

    simpleValidate() {
        var error = '';

        if (!this.model.data.name) {
            error += 'Please give this To-Do a name.';
        } 

        if(!this.model.data.whenDue) {
            if (error) {
                error += '   ';
            }

            error += 'Please say when this To-Do should be completed by.';
        } 

        if (error) {
            this.model.message = error;
            return false;
        }

        return true;
    }

    doSubmit() {
        if (!this.simpleValidate()) {
            return;
        }

        var headers = new Headers();
        var method = {} as RequestMethod;

        headers.append('Content-Type', 'application/json');

        if (this.model.data.id) {
            method = RequestMethod.Post;
        } else {
            method = RequestMethod.Put;
        }

        if (this.model.data.completed) {
            if (!this.model.data.whenCompleted) {
                this.model.data.whenCompleted = new Date();
            }
        } else {
            this.model.data.whenCompleted = undefined;
        }        

        var reqOpts = new RequestOptions({
            method: method,
            url: this._url,
            headers: headers,
            body: JSON.stringify(this.model.data)
        });

        this._ajax.request(new Request(reqOpts)).subscribe(result => {
            this.model.data = result.json() as ToDo;
            this.model.message = 'Save Successful';
            this.goBack();
        });        
    }

    doDelete() {
        var headers = new Headers();

        headers.append('Content-Type', 'application/json');

        var reqOpts = new RequestOptions({
            method: RequestMethod.Delete,
            url: this._url,
            headers: headers,
            body: JSON.stringify(this.model.data)
        });

        this._ajax.request(new Request(reqOpts)).subscribe(result => {
            this.model.message = 'Delete Successful';
            this.goBack();
        });
    }

    goBack() {
        window.history.back();
    }
}

interface ToDoDetailModel {
    data: ToDo;
    message: string;
}