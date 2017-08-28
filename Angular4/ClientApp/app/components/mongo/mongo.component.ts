import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { RestaurantModel } from '../../models/interfaces';

@Component({
    selector: 'mongo',
    templateUrl: './mongo.component.html'
})

export class MongoComponent {
    ajax: Http;

    public model: RestaurantModel;

    constructor(http: Http, route: ActivatedRoute) {
        var me = this;
        me.ajax = http;

        route.params.subscribe(params => {
            var page = + params['page']; // (+) converts string 'id' to a number
            me.getPageData(page);
        });
    }

    getPageData(page : number) {
        var url = '/api/Restaurants';

        if (page) {
            url = url + '?page=' + page;
        }

        this.ajax.get(url).subscribe(result => {
            this.model = result.json() as RestaurantModel;
        });

        return false;
    }
}