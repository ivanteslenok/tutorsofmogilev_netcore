import { connect } from 'react-redux';
import * as tutorAC from '../AC/tutor';
import CrudDataGrid from '../components/CrudDataGrid/index';

const mapStateToProps = (state, ownProps) => {
  return {
    columns: state.tutors.phonesGridColumns,
    rows: ownProps.items,
    editId: ownProps.editId
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getDeleteText: deleteRow => {
      if (deleteRow) {
        return `${deleteRow.number} ${deleteRow.operator || ''}`;
      }
      return '';
    },
    remove: id => {
      dispatch(tutorAC.removePhone(id));
    },
    create: phone => {
      dispatch(tutorAC.addPhone(phone));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(CrudDataGrid);
