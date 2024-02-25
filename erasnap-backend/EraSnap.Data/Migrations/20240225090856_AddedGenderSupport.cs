using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EraSnap.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedGenderSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"));

            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"));

            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"));

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Prompt",
                newName: "ManText");

            migrationBuilder.AddColumn<string>(
                name: "FemaleText",
                table: "Prompt",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Prompt",
                columns: new[] { "Id", "ExampleImagePath", "FemaleText", "ManText", "Name", "UserPrompt" },
                values: new object[,]
                {
                    { new Guid("01d854d5-fc38-481a-8183-d9e4d98cbd40"), "ecfe5afa-ed76-4472-b38d-5ba2e0fe5591_image.jpg", "A baroness of European heritage from the Middle Ages stands stoically in the center of the frame, her face visible and detailed. She is dressed in historically accurate attire for the period, with intricate fabrics and designs indicative of her noble status. The background, though slightly blurred due to the camera settings mimicking a Canon EOS 5D Mark IV with an aperture of f/5.6, a shutter speed of 1/125 second, and ISO 100, resembles the court of a castle, with stone walls and medieval architecture faintly discernible. The lighting is soft and natural, highlighting the baroness's features and the textures of her clothing, while the castle's details are elegantly suggested in the backdrop. The image is vivid, colored, and photorealistic, capturing the essence and atmosphere of the Middle Ages without any modern elements, ensuring the scene's historical accuracy in attire and surroundings. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Generate a vivid, colored photorealistic image of a knight of european heritage, from the middle ages, standing stoically in the center of the frame, holding his helmet under his arm, his face is visible an detailed, no helmet or hat on his head. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles the court of a castle. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the middle ages. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Középkor", false },
                    { new Guid("5e0b4c19-3c14-467e-8099-a850f16274dc"), "bf16162b-279a-40a5-b3ca-bed7748e68e5_image.jpg", "Create a colored, realistic portrait of a World War II woman soldier of Caucasian ancestry, positioned centrally within the composition, her face visible and detailed. The soldier is a woman in her late twenties, embodying a look of solemn determination. The background is a blurred yet distinct representation of a chaotic battlefield, with ominous gray smoke rising and the distant silhouettes of war machinery barely visible. The uniform's texture and details, along with the facial features of the soldier and the battlefield elements, should be rendered with a high degree of realism, capturing the gravity of the wartime period. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Create a colored, realistic portrait of a World War II soldier donning a Hungarian uniform. Position the soldier centrally within the composition, his face visible and detailed. The soldier, a caucasian male in his late twenties, with a mustache, displays a look of solemn determination. The blurred backdrop mirrors the chaotic tumult of the battlefield, with the ominous gray smoke rising, earthen trenches dug in haste, and the distant silhouette of war machinery. The texture and details of the uniform, facial features, and battlefield should be rendered in a high degree of realism, reflecting the gravity of the wartime period.  Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Második világháború", false },
                    { new Guid("7d5fddab-ff3a-4677-9cb3-e28246121f1c"), "0b2942ec-93ac-4c23-be54-1389ed3a0c6e_image.jpg", "A portrait of a high-standing woman in the Ottoman Empire, positioned centrally within the composition, her face visible and detailed. She wears extravagant, traditional Ottoman attire, embodying the wealth and status of her position. The background is intentionally blurred to focus on her, but it hints at the luxurious interior of an Ottoman palace, with elements like intricate rugs, ornate furniture, and rich tapestries, all suggesting the grandeur of the time. Attention is paid to the accuracy of her clothing, jewelry, and the setting, ensuring a reflection of the historical period without modern intrusions. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame, his face visible and detailed. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Oszmán birodalom", false },
                    { new Guid("ff6781e7-0353-4bdc-9698-5834089dc6be"), "85547a9c-35e9-4476-9b4d-709a8203e7e3_image.jpg", "An Egyptian priestess stands stoically in the center of the frame, her face visible and detailed. She is dressed in historically accurate attire for ancient Egypt, featuring traditional garments with intricate designs, likely made of linen, adorned with jewelry typical of the era such as necklaces, bracelets, and earrings made of gold and semi-precious stones. Her hair is styled according to the period, possibly with braids or a wig adorned with decorative elements. The background resembles the court of an Egyptian temple, with hieroglyphics, statues of gods, and architectural details characteristic of ancient Egyptian temples. The lighting is soft and natural, resembling the illumination one might expect from the sun filtering through an opening in the temple, highlighting the priestess's features and the textures of her clothing. The image is vivid, colored, and photorealistic, capturing the essence and atmosphere of ancient Egypt, ensuring the scene's historical accuracy in attire and surroundings. The camera settings mimic a Canon EOS 5D Mark IV with an aperture of f/5.6, a shutter speed of 1/125 second, and ISO 100, resulting in a slightly blurred background to focus attention on the priestess. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "A photo realistic Egyptian pharaoh sitting in his throne stoically in the center of the frame, his face visible and detailed. He is dressed in historically accurate attire for ancient Egypt, featuring traditional garments with intricate designs, likely made of linen, adorned with jewelry typical of the era such as necklaces, bracelets, and earrings made of gold and semi-precious stones. The background resembles the pharaoh's throne room, with hieroglyphics, statues of gods, and architectural details characteristic of ancient Egyptian interior. The lighting is soft and natural, resembling the illumination one might expect from the sun filtering through an opening in the temple, highlighting the pharaoh's features and the textures of his clothing. The image is vivid, colored, and photorealistic, capturing the essence and atmosphere of ancient Egypt, ensuring the scene's historical accuracy in attire and surroundings. The camera settings mimic a Canon EOS 5D Mark IV with an aperture of f/5.6, a shutter speed of 1/125 second, and ISO 100, resulting in a slightly blurred background to focus attention on the priestess. Please make sure there are no extra limbs or fingers and there is no text or numbers on the image.", "Egyiptom", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("01d854d5-fc38-481a-8183-d9e4d98cbd40"));

            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("5e0b4c19-3c14-467e-8099-a850f16274dc"));

            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("7d5fddab-ff3a-4677-9cb3-e28246121f1c"));

            migrationBuilder.DeleteData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("ff6781e7-0353-4bdc-9698-5834089dc6be"));

            migrationBuilder.DropColumn(
                name: "FemaleText",
                table: "Prompt");

            migrationBuilder.RenameColumn(
                name: "ManText",
                table: "Prompt",
                newName: "Text");

            migrationBuilder.InsertData(
                table: "Prompt",
                columns: new[] { "Id", "ExampleImagePath", "Name", "Text", "UserPrompt" },
                values: new object[,]
                {
                    { new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"), "0b2942ec-93ac-4c23-be54-1389ed3a0c6e_image.jpg", "Oszmán birodalom", "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.", false },
                    { new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"), "bf16162b-279a-40a5-b3ca-bed7748e68e5_image.jpg", "Második világháború", "Create a colored, realistic portrait of a World War II soldier donning a Hungarian uniform. Position the soldier centrally within the composition. The soldier, a caucasian male in his late twenties, with a mustache, displays a look of solemn determination. The blurred backdrop mirrors the chaotic tumult of the battlefield, with the ominous gray smoke rising, earthen trenches dug in haste, and the distant silhouette of war machinery. The texture and details of the uniform, facial features, and battlefield should be rendered in a high degree of realism, reflecting the gravity of the wartime period. Please make sure there is no text or any unrelated artifacts on the image. \n", false },
                    { new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"), "ecfe5afa-ed76-4472-b38d-5ba2e0fe5591_image.jpg", "Középkor", "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.", false }
                });
        }
    }
}
