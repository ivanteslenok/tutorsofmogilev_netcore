import { connect } from 'react-redux';
import { getSortedContactTypes } from './../selectors/contactTypes';
import * as contactTypeAC from '../AC/contactType';
import * as dialogsAC from '../AC/dialogs';
import EnityList from '../components/EnityList';

const mapStateToProps = state => {
  return {
    items: getSortedContactTypes(state),
    loading: state.contactTypes.loading,
    loaded: state.contactTypes.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(contactTypeAC.loadContactTypes());
    },
    remove: id => {
      dispatch(contactTypeAC.removeContactType(id));
    },
    create: contactType => {
      dispatch(contactTypeAC.createContactType(contactType));
    },
    update: (id, contactType) => {
      dispatch(contactTypeAC.updateContactType(id, contactType));
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
