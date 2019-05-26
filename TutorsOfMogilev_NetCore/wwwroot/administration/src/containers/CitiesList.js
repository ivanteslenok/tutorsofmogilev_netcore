import { connect } from 'react-redux';
import { getSortedCities } from './../selectors/cities';
import cityAC from '../AC/city';
import * as dialogsAC from '../AC/dialogs';
import EntityList from '../components/EntityList';

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
)(EntityList);
