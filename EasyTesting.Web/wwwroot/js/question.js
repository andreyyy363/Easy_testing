document.getElementById("addAnswer").addEventListener("click", function () {
    const answerBlock = document.createElement("div");
    answerBlock.innerHTML = `
        <input type="text" placeholder="Option text" class="form-control option-text" />
        <label><input type="checkbox" class="is-correct" /> Correct</label>
    `;
    document.getElementById("answers").appendChild(answerBlock);
});

document.getElementById("questionForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const text = document.querySelector('[name="NewQuestion.Text"]').value;
    const subjectId = document.getElementById("subjectSelect").value;

    const options = Array.from(document.querySelectorAll("#answers > div")).map(div => ({
        optionText: div.querySelector(".option-text").value,
        isCorrect: div.querySelector(".is-correct").checked
    }));

    const response = await fetch("/api/v1/questions", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ text, subjectId: parseInt(subjectId), answerOptions: options })
    });

    if (response.ok) {
        alert("Question created!");
        location.reload();
    } else {
        alert("Failed to create question.");
    }
});
