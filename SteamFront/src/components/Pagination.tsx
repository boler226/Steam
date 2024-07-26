import React from 'react';

interface PaginationProps {
    current: number;
    pageSize: number;
    total: number;
    onChange: (page: number, pageSize?: number) => void;
}

const Pagination: React.FC<PaginationProps> = ({ current, pageSize, total, onChange }) => {
    const totalPages = Math.ceil(total / pageSize);

    const handlePageChange = (page: number) => {
        onChange(page, pageSize);
    };

    const renderPageNumbers = () => {
        const pageNumbers = [];
        for (let i = 1; i <= totalPages; i++) {
            pageNumbers.push(
                <button
                    key={i}
                    onClick={() => handlePageChange(i)}
                    style={{
                        padding: '5px 10px',
                        margin: '0 2px',
                        border: '1px solid #ccc',
                        backgroundColor: current === i ? 'rgba(255,255,255,0.5)' : '#282B31',
                        color: current === i ? '#000' : 'rgba(255,255,255,0.5)',
                        borderRadius: '3px',
                        cursor: 'pointer',
                    }}
                >
                    {i}
                </button>
            );
        }
        return pageNumbers;
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center',padding: '10px',
            backgroundColor: '#383D46', borderRadius: '5px'}}>
            <div style={{ display: 'flex', gap: '5px' }}>
                {renderPageNumbers()}
            </div>
        </div>
    );
};

export default Pagination;
