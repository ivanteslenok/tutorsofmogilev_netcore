import { connect } from 'react-redux';
import { getSortedCities } from './../selectors/cities';
import cityAC from '../AC/city';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedCities(state),
    loading: state.cities.loading,
    loaded: state.cities.loaded,
    loadingFailed: state.cities.loadingFailed
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(cityAC.loadEntities());
    },
    remove: id => {
      dispatch(cityAC.removeEntity(id));
    },
    create: city => {
      dispatch(cityAC.createEntity(city));
    },
    update: (id, city) => {
      dispatch(cityAC.updateEntity(id, city));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EntityList);
