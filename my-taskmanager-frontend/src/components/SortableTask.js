import { useSortable } from '@dnd-kit/sortable';
import { CSS } from '@dnd-kit/utilities';

export default function SortableTask({ id, task, onDetailClick }) {
  const { attributes, listeners, setNodeRef, transform, transition } = useSortable({ id });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    border: '1px solid #ccc',
    padding: '10px',
    margin: '5px 0',
    background: '#f9f9f9',
    userSelect: 'none',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    cursor: 'pointer',
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      {...attributes}
      onClick={() => {
        console.log('Task clicked:', task.id);
        onDetailClick();
      }}
    >
      <div style={{ flexGrow: 1 }}>
        <strong>{task.title}</strong>
        <div style={{ fontSize: '0.9em', color: '#666' }}>{task.description}</div>
      </div>

      {/* Drag handle — small area, won't block click */}
      <span
        {...listeners}
        style={{
          cursor: 'grab',
          padding: '0 10px',
          fontWeight: 'bold',
          userSelect: 'none',
        }}
        aria-label="Drag handle"
        onClick={(e) => e.stopPropagation()} // avoid opening details when clicking drag handle
      >
        ≡
      </span>
    </div>
  );
}
