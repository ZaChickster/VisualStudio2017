import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState }  from '../store';
import * as RestaurantStore from '../store/Restaurants';

// At runtime, Redux will merge together...
type RestaurantDetailProps =
    RestaurantStore.Restaurant        // ... state we've requested from the Redux store
    & typeof RestaurantStore.actionCreators      // ... plus action creators we've requested
    & RouteComponentProps<{ id: string }>; // ... plus incoming routing parameters   

class RestaurantDetail extends React.Component<RestaurantDetailProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.getRestaurantDetails(this.props.match.params.id);
    }

    componentWillReceiveProps(nextProps: RestaurantDetailProps) {
        // This method runs when incoming props (e.g., route params) change
        this.props.getRestaurantDetails(this.props.match.params.id);
    }

    public render() {
        return <div>
            <h1>{this.props.name}</h1>
            <p>This component demonstrates editing individual item from the MongoDB.</p>
            {this.renderForm()}
        </div>;
    }

    private renderForm() {
        return <form>
            <fieldset className='scheduler-border'>
                <legend className='scheduler-border'>Details</legend>

                <div className='form-group'>
                    <label>
                        Name
                        <input type='text' className='form-control' name='name' id='name' value='{this.props.name}' />
                    </label>
                </div>
            </fieldset>
        </form>;
    }
}

export default connect(
    (state: ApplicationState) => state.details, // Selects which state properties are merged into the component's props
    RestaurantStore.actionCreators                 // Selects which action creators are merged into the component's props
)(RestaurantDetail) as typeof RestaurantDetail;
