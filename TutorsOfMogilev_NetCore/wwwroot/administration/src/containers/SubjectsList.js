import { connect } from 'react-redux';
import { getSortedSubjects } from './../selectors/subjects';
import * as subjectAC from '../AC/subject';
import * as dialogsAC from '../AC/dialogs';
import EnityList from '../components/EnityList';

const mapStateToProps = state => {
  return {
    items: getSortedSubjects(state),
    loading: state.subjects.loading,
    loaded: state.subjects.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(subjectAC.loadSubjects());
    },
    remove: id => {
      dispatch(subjectAC.removeSubject(id));
    },
    create: subject => {
      dispatch(subjectAC.createSubject(subject));
    },
    update: (id, subject) => {
      dispatch(subjectAC.updateSubject(id, subject));
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
