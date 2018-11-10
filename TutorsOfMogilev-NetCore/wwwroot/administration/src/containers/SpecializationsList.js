import { connect } from 'react-redux';
import { getSortedSpecializations } from './../selectors/specializations';
import * as specializationAC from '../AC/specialization';
import * as dialogsAC from '../AC/dialogs';
import EnityList from '../components/EnityList';

const mapStateToProps = state => {
  return {
    items: getSortedSpecializations(state),
    loading: state.specializations.loading,
    loaded: state.specializations.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(specializationAC.loadSpecializations());
    },
    remove: id => {
      dispatch(specializationAC.removeSpecialization(id));
    },
    create: specialization => {
      dispatch(specializationAC.createSpecialization(specialization));
    },
    update: (id, specialization) => {
      dispatch(specializationAC.updateSpecialization(id, specialization));
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
