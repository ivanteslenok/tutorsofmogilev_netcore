import { connect } from 'react-redux';
import { getSortedSpecializations } from './../selectors/specializations';
import specializationAC from '../AC/specialization';
import * as dialogsAC from '../AC/dialogs';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedSpecializations(state),
    loading: state.specializations.loading,
    loaded: state.specializations.loaded,
    loadingFailed: state.specializations.loadingFailed,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(specializationAC.loadEntities());
    },
    remove: id => {
      dispatch(specializationAC.removeEntity(id));
    },
    create: specialization => {
      dispatch(specializationAC.createEntity(specialization));
    },
    update: (id, specialization) => {
      dispatch(specializationAC.updateEntity(id, specialization));
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
