import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState }  from '../store';
import * as RestaurantState from '../store/Restaurants';

// At runtime, Redux will merge together...
type RestaurantsProps =
    RestaurantState.RestaurantState        // ... state we've requested from the Redux store
    & typeof RestaurantState.actionCreators      // ... plus action creators we've requested
    & RouteComponentProps<{ currentPage: string }>; // ... plus incoming routing parameters   

class Restaurants extends React.Component<RestaurantsProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        let currentPage = parseInt(this.props.match.params.currentPage) || 0;
        this.props.requestRestaurants(currentPage);
    }

    componentWillReceiveProps(nextProps: RestaurantsProps) {
        // This method runs when incoming props (e.g., route params) change
        let currentPage = parseInt(nextProps.match.params.currentPage) || 0;
        this.props.requestRestaurants(currentPage);
    }

    public render() {
        return <div>
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
            {this.renderPagination()}
            {this.renderRestaurants()}
        </div>;
    }

    private renderRestaurants() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Borough</th>
                    <th>Cuisine</th>
                </tr>
            </thead>
            <tbody>
            {this.props.pageInstances.map(restaurant =>
                <tr key={restaurant.restaurant_id}>
                    <td>{restaurant.name}</td>
                    <td>{restaurant.borough}</td>
                    <td>{restaurant.cuisine}</td>
                </tr>
            )}
            </tbody>
        </table>;
    }

    private renderPagination() {
        let prevPage = this.props.currentPage - 1;
        let nextPage = this.props.currentPage + 1;
        
        return <p className='clearfix text-center'>
            {prevPage >= 0 ? <Link className='btn btn-default pull-left' to={`/mongodb/${prevPage}`}>Previous</Link> : []}
            <Link className='btn btn-default pull-right' to={`/mongodb/${nextPage}` }>Next</Link>
            { this.props.isLoading ? <span>Loading...</span> : [] }
        </p>;
    }
}

export default connect(
    (state: ApplicationState) => state.restaurants, // Selects which state properties are merged into the component's props
    RestaurantState.actionCreators                 // Selects which action creators are merged into the component's props
)(Restaurants) as typeof Restaurants;
