import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Counter from './components/Counter';
import Restaurants from './components/Restaurants';
import RestaurantDetail from './components/RestaurantDetail';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
    <Route path='/mongodb/details/:id?' component={RestaurantDetail} />
    <Route path='/mongodb/:currentPage?' component={Restaurants} />
</Layout>;
