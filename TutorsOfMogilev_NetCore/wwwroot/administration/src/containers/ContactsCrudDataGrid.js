import { connect } from 'react-redux';
import * as tutorAC from '../AC/tutor';
import contactTypesAC from '../AC/contactType';
import CrudDataGrid from '../components/CrudDataGrid/index';

const emptyAvailableValue = { name: '-' };

const mapStateToProps = (state, ownProps) => {
  return {
    columns: state.tutors.contactsGridColumns,
    availableColumnsValues: {
      contactType: [emptyAvailableValue, ...state.contactTypes.items]
    },
    availableColumnsValuesLoaded: state.contactTypes.loaded,
    defaultValues: {
      contactType: emptyAvailableValue
    },
    rows: ownProps.items,
    editId: ownProps.editId
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadAvailableValues: () => {
      dispatch(contactTypesAC.loadEntities());
    },
    getDeleteText: deleteRow => {
      if (deleteRow) {
        return `${deleteRow.name ||
          (deleteRow.contactType ? deleteRow.contactType.name : '')}: ${
          deleteRow.value
        }`;
      }
      return '';
    },
    remove: id => {
      dispatch(tutorAC.removeContact(id));
    },
    create: contact => {
      contact.contactType = contact.contactType.id ? contact.contactType : null;
      dispatch(tutorAC.addContact(contact));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(CrudDataGrid);
