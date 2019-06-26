import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantModel } from '../../models/interfaces';
import { MongoDataService } from '../../services/mongodata.service';

@Component({
    selector: 'mongo',
    templateUrl: './mongo.component.html'
})

export class MongoComponent {
    service: MongoDataService;
    model: RestaurantModel;

    constructor(mongo: MongoDataService, route: ActivatedRoute) {
        const me = this;
        me.service = mongo;

        route.params.subscribe(params => {
            const page = + params['page']; // (+) converts string 'id' to a number
            me.getPageData(page);
        });
    }

    getPageData(page : number) {
        this.service.getMongoPage(page).subscribe((page: RestaurantModel) => {
            this.model = page;
        });
    }
}