import React, { useState } from 'react';
import "./css/style.css";

function App() {
	const [answers, setAnswers] = useState({
		colorful: '0',
		smell: '0',
		stemColorChange: '0',
		stemThickening: '0',
		insects: '0',
	});
	const [result, setResult] = useState(null);

	const handleChange = (event) => {
		const { name, value } = event.target;
		setAnswers((prev) => ({ ...prev, [name]: value }));
	};

	const handleSubmit = async (event) => {
		event.preventDefault();
		const features = Object.values(answers).map((value) => parseInt(value, 10));
		
		try {
			const response = await fetch('http://localhost:5081/api/mushroom/classify', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify(features),
			});
			
			const data = await response.text();
			setResult(data);
		} catch (error) {
			console.error('Error:', error);
		}
	};

	return (
		<div>
			<h1>Класифікація грибів</h1>
			<form onSubmit={handleSubmit}>
				<div>
					<label>Яскраве забарвлення:</label>
					<select name="colorful" value={answers.colorful} onChange={handleChange}>
						<option value="0">Так</option>
						<option value="1">Ні</option>
					</select>
				</div>
				<div>
					<label>Запах:</label>
					<select name="smell" value={answers.smell} onChange={handleChange}>
						<option value="0">Неприємний та різкий</option>
						<option value="1">Приємний</option>
					</select>
				</div>
				<div>
					<label>Зміна кольору при зрізанні на сині/фіолетові відтінки:</label>
					<select name="stemColorChange" value={answers.stemColorChange} onChange={handleChange}>
						<option value="0">Так</option>
						<option value="1">Ні</option>
					</select>
				</div>
				<div>
					<label>Ніжка товстіє донизу:</label>
					<select name="stemThickening" value={answers.stemThickening} onChange={handleChange}>
						<option value="0">Так</option>
						<option value="1">Ні</option>
					</select>
				</div>
				<div>
					<label>Реакція комах на гриб:</label>
					<select name="insects" value={answers.insects} onChange={handleChange}>
						<option value="0">Немає</option>
						<option value="1">Уникають</option>
					</select>
				</div>
				<button type="submit">Класифікувати</button>
			</form>
			{result && <p>Результат: {result}</p>}
		</div>
	);
}

export default App;