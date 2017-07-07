import { Component } from '@angular/core';
import { Http, Request, RequestOptions, RequestMethod, Headers } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Restaurant } from '../../models/interfaces';

@Component({
    selector: 'mongodetails',
    templateUrl: './mongodetails.component.html'
})
export class MongoDetailsComponent {
    model: MongoDetailModel;

    _http: Http;
    _route: ActivatedRoute;
    _url: string;

    constructor(http: Http, route: ActivatedRoute, location: Location) {
        var me = this;

        me.model = {} as MongoDetailModel;
        me._http = http;
        me._route = route;
        me._url = '/api/Restaurant';

        route.params.subscribe(params => {
            me.updateModel(params['id'])
        });
    }

    updateModel(id: string) {
        var url = '';

        if (id) {
            url = this._url + '?id=' + id;

            this._http.get(url).subscribe(result => {
                this.model.data = result.json() as Restaurant;
            });
        } else {
            this.model.data = {} as Restaurant;
        }        
    }

    doSubmit() {
        var headers = new Headers();
        var method = {} as RequestMethod;

        headers.append('Content-Type', 'application/json');

        if (this.model.data.restaurant_id) {
            method = RequestMethod.Post;
        } else {
            method = RequestMethod.Put;
        }

        var reqOpts = new RequestOptions({
            method: method,
            url: this._url,
            headers: headers,
            body: JSON.stringify(this.model.data)
        });

        this._http.request(new Request(reqOpts)).subscribe(result => {
            this.model.data = result.json() as Restaurant;
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

        this._http.request(new Request(reqOpts)).subscribe(result => {
            this.model.message = 'Delete Successful';
            this.goBack();
        });
    }

    goBack() {
        window.history.back();
    }
}

interface MongoDetailModel {
    data: Restaurant;
    message: string;
}