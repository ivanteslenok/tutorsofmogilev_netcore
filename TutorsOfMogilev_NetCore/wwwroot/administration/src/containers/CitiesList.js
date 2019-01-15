import { connect } from 'react-redux';
import { getSortedCities } from './../selectors/cities';
import * as cityAC from '../AC/city';
import * as dialogsAC from '../AC/dialogs';
import EnityList from '../components/EnityList';

const mapStateToProps = state => {
  return {
    items: getSortedCities(state),
    loading: state.cities.loading,
    loaded: state.cities.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(cityAC.loadCities());
    },
    remove: id => {
      dispatch(cityAC.removeCity(id));
    },
    create: city => {
      dispatch(cityAC.createCity(city));
    },
    update: (id, city) => {
      dispatch(cityAC.updateCity(id, city));
    },
    openDeleteDialog: () => {
      dispatch(dialogsAC.openDeleteConfirmDialog());
    },
    closeDeleteDialog: () => {
      dispatch(dialogsAC.closeDeleteConfirmDialog());
    },
    openInputDialog: () => {
      dispatch(dialogsAC.openDialogWithInput());
    },
    closeInputDialog: () => {
      dispatch(dialogsAC.closeDialogWithInput());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EnityList);
