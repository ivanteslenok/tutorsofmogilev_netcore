import { connect } from 'react-redux';
import { getSortedSubjects } from './../selectors/subjects';
import subjectAC from '../AC/subject';
import * as dialogsAC from '../AC/dialogs';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedSubjects(state),
    loading: state.subjects.loading,
    loaded: state.subjects.loaded,
    loadingFailed: state.subjects.loadingFailed,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(subjectAC.loadEntities());
    },
    remove: id => {
      dispatch(subjectAC.removeEntity(id));
    },
    create: subject => {
      dispatch(subjectAC.createEntity(subject));
    },
    update: (id, subject) => {
      dispatch(subjectAC.updateEntity(id, subject));
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
